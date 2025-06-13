using System.ComponentModel.DataAnnotations;

namespace iLoveIbadah.Website.Models
{
    public class CommentLikeVM
    {
    }

    public class CreateCommentLikeVM
    {
        [Required]
        public int CommentId { get; set; }
        [Required]
        public int UserAccountId { get; set; }
    }
}
