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
    public class CommentLikeRepository : GenericRepository<CommentLike>, ICommentLikeRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public CommentLikeRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetCommentLikeCount(int commentId)
        {
            var count = await _dbContext.CommentLikes.Where(q => q.CommentId == commentId).CountAsync();
            return count;
        }

        public async Task<bool> Exists(int commentId, int userAccountId)
        {
            return await _dbContext.CommentLikes.AnyAsync(q => q.CommentId == commentId && q.UserAccountId == userAccountId);
        }
    }
}
