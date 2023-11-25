using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace OnlineLibrary.Pages
{
    public class LoginModel : PageModel
    {
        private readonly LibraryDbContext libraryDbContext;
        private readonly ILogger<LoginModel> logger;

        [BindProperty]
        public Login LoginInput { get; set; }

        public LoginModel(LibraryDbContext libraryDbContext, ILogger<LoginModel> logger)
        {
            this.libraryDbContext = libraryDbContext;
            this.logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await libraryDbContext.Users
                    .FirstOrDefaultAsync(u => u.Email == LoginInput.Email);

                if (user == null)
                {
                    logger.LogInformation("Login failed: User does not exist.");
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                if (user != null && BCrypt.Net.BCrypt.Verify(LoginInput.Password, user.PasswordHash))
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("Authority", user.Authority)
                    // Add additional claims as needed
                };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return LocalRedirect("/index");
                }
                else
                {
                    logger.LogInformation("User failed to login.");
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            return Page();
        }

    
    }

}
