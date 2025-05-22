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
    public class GetBlogListRequestHandler : IRequestHandler<GetBlogListRequest, List<BlogListDto>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public GetBlogListRequestHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<List<BlogListDto>> Handle(GetBlogListRequest request, CancellationToken cancellationToken)
        {
            var blogs = await _blogRepository.GetAll();
            return _mapper.Map<List<BlogListDto>>(blogs);
        }
    }
}
