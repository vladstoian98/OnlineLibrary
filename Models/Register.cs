using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Register
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Authority { get; set; }
    }
}
