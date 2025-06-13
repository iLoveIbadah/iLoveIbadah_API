using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace iLoveIbadah.Website.Models
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public int BlobFileId { get; set; } // Thumbnail!!!
        //public BlobFileVM? BlobFile { get; set; } // Thumbnail!!!
        public int TotalViews { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class BlogListVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BlobFileId { get; set; } // Thumbnail!!!
        public int TotalViews { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    // Temporary solution cause i don't return comments with blog details in api need to do this another time no time for deadline BAD Project
    public class BlogWithCommentsVM
    {
        public BlogVM Blog { get; set; }
        public List<CommentListVM> Comments { get; set; }
        public CreateCommentVM CreateComment { get; set; } = new();
        public UpdateCommentVM UpdateComment { get; set; } = new();
    }

    public class CreateBlogVM
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Slug { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int BlobFileId { get; set; }
    }

    public class UpdateBlogVM
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public int BlobFileId { get; set; } // Thumbnail!!!
    }
}
