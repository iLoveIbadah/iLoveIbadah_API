using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Domain
{
    public class CommentLike
    {
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        [ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
