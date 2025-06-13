using System.ComponentModel.DataAnnotations;

namespace iLoveIbadah.Website.Models
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class CategoryListVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class CreateCategoryVM
    {
        [Required] public string FullName { get; set; }
    }

    public class UpdateCategoryVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}