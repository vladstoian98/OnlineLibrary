using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

namespace OnlineLibrary.Pages
{
    [Authorize]
    public class AuthorBooksModel : PageModel
    {
        private readonly LibraryDbContext libraryDbContext;

        public AuthorBooksModel(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public IList<Book> Books { get; set; }
        public string AuthorName { get; set; }

        public void OnGet(string authorName)
        {
            AuthorName = authorName;
            Books = libraryDbContext.Books
                .Where(b => b.Author == authorName)
                .ToList();
        }

        public string GetPdfDownloadUrl(string pdfPath)
        {
            var fullPath = Url.Content(Path.Combine("~/", pdfPath));
            return fullPath;
        }
    }
}