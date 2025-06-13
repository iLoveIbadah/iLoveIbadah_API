using System.ComponentModel.DataAnnotations;

namespace iLoveIbadah.Website.Models
{
    public class BlobFileVM
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public string FullName { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public int CreatedBy { get; set; }
    }

    public class CreateBlobFileVM
    {
        [Required]
        public string Uri { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public int CreatedBy { get; set; }
    }
}