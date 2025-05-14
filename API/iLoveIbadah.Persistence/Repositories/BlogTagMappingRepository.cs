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
    public class BlogTagMappingRepository : GenericRepository<BlogTagMapping>, IBlogTagMappingRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public BlogTagMappingRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tag>> GetTagsByBlog(int blogId)
        {
            var tags = await _dbContext.BlogTagMappings
                .Where(q => q.BlogId == blogId)
                .Select(q => q.Tag)
                .ToListAsync();
            return tags;
        }

        public async Task<List<Blog>> GetBlogsByTag(int tagId)
        {
            var blogs = await _dbContext.BlogTagMappings
                .Where(q => q.TagId == tagId)
                .Select(q => q.Blog)
                .ToListAsync();
            return blogs;
        }

        public async Task<bool> Exists(int blogId, int tagId)
        {
            return await _dbContext.BlogTagMappings.AnyAsync(q => q.BlogId == blogId && q.TagId == tagId);
        }
    }
}
