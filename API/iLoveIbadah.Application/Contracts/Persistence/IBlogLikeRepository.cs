using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface IBlogLikeRepository : IGenericRepository<BlogLike>
    {
        Task<int> GetBlogLikeCount(int blogId);
        Task<bool> Exists(int blogId, int userAccountId);
    }
}
