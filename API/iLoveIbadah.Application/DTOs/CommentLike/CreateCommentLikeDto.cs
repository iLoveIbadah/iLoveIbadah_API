using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.CommentLike
{
    public class CreateCommentLikeDto
    {
        public int CommentId { get; set; }
        public int UserAccountId { get; set; }
    }
}
