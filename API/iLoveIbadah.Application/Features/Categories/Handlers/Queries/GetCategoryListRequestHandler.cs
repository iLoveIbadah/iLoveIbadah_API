using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.Category;
using iLoveIbadah.Application.Features.Categories.Requests.Queries;
using MediatR;

namespace iLoveIbadah.Application.Features.Categories.Handlers.Queries
{
    public class GetCategoryListRequestHandler : IRequestHandler<GetCategoryListRequest, List<CategoryListDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryListRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryListDto>> Handle(GetCategoryListRequest request,
            CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll();
            return _mapper.Map<List<CategoryListDto>>(categories);
        }
    }
}
