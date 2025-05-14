using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<Comment> GetCommentWithDetails(int id);
        Task<List<Comment>> GetCommentsWithDetailsByBlog(int blogId);
        Task<List<Comment>> GetCommentsWithDetailsByByParentComment(int parentCommentId);
    }
}
