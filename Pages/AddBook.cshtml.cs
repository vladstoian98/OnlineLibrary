using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using System.Security.Claims;

namespace OnlineLibrary.Pages
{
    [Authorize(Policy = "RequireAuthorRole")]
    public class AddBookModel : PageModel
    {
        [BindProperty]
        public Book Book { get; set; }

        private readonly LibraryDbContext libraryDbContext;

        public bool IsAuthor { get; private set; }

        public AddBookModel(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public void OnGet()
        {
            var value = HttpContext.User.Claims.ElementAtOrDefault(1).Value;
            IsAuthor = User.Identity.IsAuthenticated && HttpContext.User.Claims.ElementAtOrDefault(1).Value == "Author";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Book.UploadedPdf != null && Book.UploadedPdf.Length > 0)
            {
                // Save the PDF file
                var folderName = Path.Combine("wwwroot", "pdfs");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var fileName = $"{Guid.NewGuid()}_{Book.UploadedPdf.FileName}";
                var fullPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    Book.UploadedPdf.CopyTo(stream);
                }

                Book.PdfPath = Path.Combine("pdfs", fileName);

                libraryDbContext.Books.Add(Book);

                Book.AddDate = DateTime.SpecifyKind(Book.AddDate, DateTimeKind.Utc);

                libraryDbContext.SaveChanges();

             
            }

            return RedirectToPage("/index");
        }
    }
}
