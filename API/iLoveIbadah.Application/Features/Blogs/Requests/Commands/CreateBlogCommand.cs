using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Responses;
using iLoveIbadah.Application.DTOs.Blog;
using MediatR;

namespace iLoveIbadah.Application.Features.Blogs.Requests.Commands
{
    public class CreateBlogCommand : IRequest<BaseCommandResponse>
    {
        public CreateBlogDto BlogDto { get; set; }
    }
}
