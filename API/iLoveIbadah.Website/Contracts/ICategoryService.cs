using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;

namespace iLoveIbadah.Website.Contracts
{
    public interface ICategoryService
    {
        Task<List<CategoryListVM>> GetAll();
        //Task<CategoryVM> GetById(int id);
        //Task<Response<int>> Create(CategoryVM category);
        //Task<Response<int>> Update(CategoryVM category);
        //Task<Response<int>> Delete(int id);
    }
}
