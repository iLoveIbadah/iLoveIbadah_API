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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;

        public CommentRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Comment> GetCommentWithDetails(int id)
        {
            var comment = await _dbContext.Comments
                .FirstOrDefaultAsync(q => q.Id == id);
            return comment;
        }

        public async Task<List<Comment>> GetCommentsWithDetailsByBlog(int blogId)
        {
            var comments = await _dbContext.Comments
                .Where(q => q.BlogId == blogId && q.ParentCommentId == null)
                .ToListAsync();
            return comments;
        }

        public async Task<List<Comment>> GetCommentsWithDetailsByByParentComment(int parentCommentId)
        {
            var comments = await _dbContext.Comments
                .Where(q => q.ParentCommentId == parentCommentId)
                .ToListAsync();
            return comments;
        }
    }
}
