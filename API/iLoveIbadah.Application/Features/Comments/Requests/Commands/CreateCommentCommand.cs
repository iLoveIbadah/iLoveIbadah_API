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
    public class CreateCommentCommand : IRequest<BaseCommandResponse>
    {
        public CreateCommentDto CommentDto { get; set; }
    }
}
