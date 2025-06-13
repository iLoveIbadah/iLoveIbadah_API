using AutoMapper;
using iLoveIbadah.Website.Contracts;
using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;

namespace iLoveIbadah.Website.Services
{
    public class BlogService : BaseHttpService, IBlogService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;

        public BlogService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<Response<int>> Create(CreateBlogVM blog)
        {
            try
            {
                var response = new Response<int>();
                CreateBlogDto blogDto = _mapper.Map<CreateBlogDto>(blog);
                AddBearerToken();
                var apiResponse = await _client.BlogsPOSTAsync(blogDto);
                if (!apiResponse.Success)
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }

                response.Data = apiResponse.Id;
                response.Success = true;
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        //public async Task<Response<int>> Delete(int id)
        //{
        //    try
        //    {
        //        AddBearerToken();
        //        await _client.BlogsDELETEAsync(id);
        //        return new Response<int> { Success = true };
        //    }
        //    catch (ApiException ex)
        //    {
        //        return ConvertApiExceptions<int>(ex);
        //    }
        //}

        public async Task<BlogVM> GetById(int id)
        {
            var blog = await _client.BlogsGETAsync(id);
            return _mapper.Map<BlogVM>(blog);
        }

        public async Task<List<BlogListVM>> GetAll()
        {
            var blogs = await _client.BlogsAllAsync();
            return _mapper.Map<List<BlogListVM>>(blogs);
        }
    }
}
