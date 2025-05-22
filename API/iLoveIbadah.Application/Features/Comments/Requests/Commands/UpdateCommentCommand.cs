using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.Comment;
using iLoveIbadah.Application.Responses;
using MediatR;

namespace iLoveIbadah.Application.Features.Comments.Requests.Commands
{
    public class UpdateCommentCommand : IRequest<Unit>
    {
        //public int Id { get; set; }
        //public string Content { get; set; }
        public UpdateCommentDto CommentDto { get; set; } // I Thought the following... but not true: -> This was not needed as in business rule the only field that can be updated is the content of the comment
    }
}
