using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Password { get; set; }

    }
}
