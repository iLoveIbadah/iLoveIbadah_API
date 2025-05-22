using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.Tag;
using iLoveIbadah.Application.Features.Tags.Requests.Queries;
using MediatR;

namespace iLoveIbadah.Application.Features.Tags.Handlers.Queries
{
    public class GetTagListRequestHandler : IRequestHandler<GetTagListRequest, List<TagListDto>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        
        public GetTagListRequestHandler(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<List<TagListDto>> Handle(GetTagListRequest request, CancellationToken cancellationToken)
        {
            var tags = await _tagRepository.GetAll();
            return _mapper.Map<List<TagListDto>>(tags);
        }
    }
}
