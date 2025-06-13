using System.ComponentModel.DataAnnotations;

namespace iLoveIbadah.Website.Models
{
    public class BlogLikeVM
    {
        //TODO
    }

    public class CreateBlogLikeVM
    {
        [Required]
        public int BlogId { get; set; }
        [Required]
        public int UserAccountId { get; set; }
    }
}
