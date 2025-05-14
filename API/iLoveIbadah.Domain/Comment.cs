using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        [ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
        public string Content { get; set; }
        public DateTime? WrittenAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; } // if there is no last updated at then it means comment was never updated
        [ForeignKey("Comment")]
        public int? ParentCommentId { get; set; } // Parent Id, so is this a reply to another comment or a reply to the blog (top level)?
        public Comment? ParentComment { get; set; }
        public ICollection<Comment>? Replies { get; set; }
    }
}
