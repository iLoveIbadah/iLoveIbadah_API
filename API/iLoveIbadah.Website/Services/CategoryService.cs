using AutoMapper;
using iLoveIbadah.Website.Contracts;
using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;

namespace iLoveIbadah.Website.Services
{
    public class CategoryService : BaseHttpService, ICategoryService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;

        public CategoryService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<List<CategoryListVM>> GetAll()
        {
            var categories = await _httpClient.CategoriesAllAsync();
            return _mapper.Map<List<CategoryListVM>>(categories);
        }
    }
}
