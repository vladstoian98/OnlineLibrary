using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The author is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The genre is required.")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "The date is required.")]
        public DateTime AddDate { get; set; }

        [Required(ErrorMessage = "A PDF file is required.")]
        public string PdfPath { get; set; }

        [NotMapped] // This attribute is used if you're using Entity Framework and don't want to map this property to the database
        public IFormFile UploadedPdf { get; set; }

    }
}
