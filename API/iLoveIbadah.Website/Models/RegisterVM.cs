using System.ComponentModel.DataAnnotations;

namespace iLoveIbadah.Website.Models
{
    public class RegisterVM
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
    }
}
