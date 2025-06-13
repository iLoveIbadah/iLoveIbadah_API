using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;

namespace iLoveIbadah.Website.Contracts
{
    public interface ITagService
    {
        Task<List<TagListVM>> GetAll();
        //Task<TagVM> GetById(int id);
        //Task<Response<int>> Create(TagVM tag);
        //Task<Response<int>> Update(TagVM tag);
        //Task<Response<int>> Delete(int id);
    }
}
