using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using BCrypt.Net;

namespace OnlineLibrary.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Register RegisterInput { get; set; }

        private readonly LibraryDbContext libraryDbContext;

        public RegisterModel(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = RegisterInput.Email,
                    Authority = RegisterInput.Authority,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(RegisterInput.Password)

                };

                if(libraryDbContext.Users.FirstOrDefault(u => u.Email == user.Email) == null)
                {
                    libraryDbContext.Users.Add(user);
                    await libraryDbContext.SaveChangesAsync();

                    return RedirectToPage("/Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email already in use for another account");
                }
            }

            return Page();
        }
    }
}
