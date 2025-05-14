using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Persistence.Repositories
{
    public class BlogLikeRepository : GenericRepository<BlogLike>, IBlogLikeRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public BlogLikeRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetBlogLikeCount(int blogId)
        {
            var count = await _dbContext.BlogLikes.Where(q => q.BlogId == blogId).CountAsync();
            return count;
        }

        public async Task<bool> Exists(int blogId, int userAccountId)
        {
            return await _dbContext.BlogLikes.AnyAsync(q => q.BlogId == blogId && q.UserAccountId == userAccountId);
        }
    }
}
