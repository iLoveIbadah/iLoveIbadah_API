using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.Blog;
using iLoveIbadah.Application.Features.Blogs.Requests.Queries;
using MediatR;

namespace iLoveIbadah.Application.Features.Blogs.Handlers.Queries
{
    public class GetBlogDetailsRequestHandler : IRequestHandler<GetBlogDetailsRequest, BlogDto>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public GetBlogDetailsRequestHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<BlogDto> Handle(GetBlogDetailsRequest request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetById(request.Id);
            return _mapper.Map<BlogDto>(blog);
        }
    }
}
