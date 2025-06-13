using AutoMapper;
using iLoveIbadah.Website.Contracts;
using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;

namespace iLoveIbadah.Website.Services
{
    public class TagService : BaseHttpService, ITagService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;

        public TagService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<List<TagListVM>> GetAll()
        {
            var tags = await _httpClient.TagsAllAsync();
            return _mapper.Map<List<TagListVM>>(tags);
        }
    }
}
