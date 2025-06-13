namespace iLoveIbadah.Website.Models
{
    public class CommentVM
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int UserAccountId { get; set; }
        public string Content { get; set; }
        public DateTime? WrittenAt { get; set; }
        public int? ParentCommentId { get; set; }
        public List<CommentVM>? Replies { get; set; }
    }

    public class CommentListVM
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int UserAccountId { get; set; }
        public string Content { get; set; }
        public DateTime? WrittenAt { get; set; }
        public int? ParentCommentId { get; set; }
    }

    public class CreateCommentVM
    {
        public int BlogId { get; set; }
        public int? UserAccountId { get; set; }
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }
    }

    public class UpdateCommentVM
    {
        public int Id { get; set; }
        public int? UserAccountId { get; set; }
        public string Content { get; set; }
    }
}