using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.Comment;
using MediatR;

namespace iLoveIbadah.Application.Features.Comments.Requests.Queries
{
    public class GetCommentDetailsRequest : IRequest<CommentDto>
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int UserAccountId { get; set; }
        public string Content { get; set; }
        public DateTime? WrittenAt { get; set; }
        public int? ParentCommentId { get; set; }
        public List<CommentDto>? Replies { get; set; }
    }
}
