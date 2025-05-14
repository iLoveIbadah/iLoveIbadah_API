using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface ICommentLikeRepository : IGenericRepository<CommentLike>
    {
        Task<int> GetCommentLikeCount(int commentId);
        Task<bool> Exists(int commentId, int userAccountId);
    }
}
