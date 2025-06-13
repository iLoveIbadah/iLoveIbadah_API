using AutoMapper;
using iLoveIbadah.Website.Contracts;
using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;

namespace iLoveIbadah.Website.Services
{
    public class CommentService : BaseHttpService, ICommentService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;

        public CommentService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        // Get All for use cases like show 10 latest comments of all blogs in blog list page, needs pagination
        public async Task<List<CommentListVM>> GetAll()
        {
            var comments = await _client.CommentsAllAsync();
            return _mapper.Map<List<CommentListVM>>(comments);
        }

        public async Task<CommentVM> GetById(int id)
        {
            var comment = await _client.CommentsGETAsync(id);
            return _mapper.Map<CommentVM>(comment);
        }

        public async Task<Response<int>> Create(CreateCommentVM comment)
        {
            try
            {
                var response = new Response<int>();
                CreateCommentDto commentDto = _mapper.Map<CreateCommentDto>(comment);
                AddBearerToken();
                var apiResponse = await _client.CommentsPOSTAsync(commentDto);
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

        public async Task<Response<int>> Update(UpdateCommentVM comment)
        {
            try
            {
                UpdateCommentDto commentDto = _mapper.Map<UpdateCommentDto>(comment);
                AddBearerToken();
                await _client.CommentsPUTAsync(commentDto);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
