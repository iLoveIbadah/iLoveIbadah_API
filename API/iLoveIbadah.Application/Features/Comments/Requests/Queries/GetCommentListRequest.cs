using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.Comment;
using MediatR;

namespace iLoveIbadah.Application.Features.Comments.Requests.Queries
{
    public class GetCommentListRequest : IRequest<List<CommentListDto>>
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int UserAccountId { get; set; } // why not pass user account profile picture type id, or event the blobfile then it won't require multiple trips of request to display user profile picture in comment ? TODO investigate
        public string Content { get; set; }
        public DateTime? WrittenAt { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
