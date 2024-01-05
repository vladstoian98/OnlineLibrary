using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibrary.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize(Policy = "RequireAdminRole")]
public class BookModeration : PageModel
{
    private readonly LibraryDbContext _context;

    public BookModeration(LibraryDbContext context)
    {
        _context = context;
    }

    public List<Book> Books { get; set; }

    public async Task OnGetAsync()
    {
        Books = await _context.Books.ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book != null)
        {
            // Assuming you're storing a relative path in Book.PdfPath
            var folderName = Path.Combine("wwwroot");
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderName, book.PdfPath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            // Now remove the book entry from the database
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/BookModeration");
    }
}
