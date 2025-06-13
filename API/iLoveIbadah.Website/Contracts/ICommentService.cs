using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;

namespace iLoveIbadah.Website.Contracts
{
    public interface ICommentService
    {
        Task<List<CommentListVM>> GetAll();
        Task<CommentVM> GetById(int id);
        Task<Response<int>> Create(CreateCommentVM comment);
        Task<Response<int>> Update(UpdateCommentVM comment);
        //Task<Response<int>> Delete(int id);
    }
}
