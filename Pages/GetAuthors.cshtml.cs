using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibrary.Data;

namespace OnlineLibrary.Pages
{
    [Authorize]
    public class GetAuthorsModel : PageModel
    {
        private readonly LibraryDbContext libraryDbContext;

        public GetAuthorsModel(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public IList<string> Authors { get; set; }

        public void OnGet()
        {
            //Authors = libraryDbContext.Users
            //    .Where(u => u.Authority == "Author");

            Authors = libraryDbContext.Books
                .Select(b => b.Author)
                .Distinct()
                .ToList();
        }
    }
}