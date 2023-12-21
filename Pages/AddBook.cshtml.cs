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

        public void OnPost()
        {
            if (Book.UploadedPdf != null && Book.UploadedPdf.Length > 0)
            {
                // Generate a unique file name
                var fileName = Path.Combine("wwwroot/pdfs", $"{Guid.NewGuid()}_{Book.UploadedPdf.FileName}");

                // Save the PDF file
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    Book.UploadedPdf.CopyTo(stream);
                }

                Book.PdfPath = fileName;

                libraryDbContext.Books.Add(Book);

                Book.AddDate = DateTime.SpecifyKind(Book.AddDate, DateTimeKind.Utc);

                libraryDbContext.SaveChanges();

                RedirectToPage("/index");
            }
        }
    }
}
