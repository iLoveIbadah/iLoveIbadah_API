using System.ComponentModel.DataAnnotations;

namespace iLoveIbadah.Website.Models
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } // error from my side named it password hash because the column in db is called password hash but well in ui it isn't hashed before sending...
        public string ReturnUrl { get; set; }
    }
}
