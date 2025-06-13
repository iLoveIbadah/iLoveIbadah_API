using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;

namespace iLoveIbadah.Website.Contracts
{
    public interface IBlogService
    {
        Task<List<BlogListVM>> GetAll();
        Task<BlogVM> GetById(int id);
        Task<Response<int>> Create(CreateBlogVM blog);
        //Task<Response<int>> Update(BlogVM blog);
        //Task<Response<int>> Delete(int id);
    }
}
