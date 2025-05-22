using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Comment
{
    public class CreateCommentDto
    {
        public int BlogId { get; set; }
        public int? UserAccountId { get; set; }
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
