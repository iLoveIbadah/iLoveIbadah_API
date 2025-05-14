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
    public class BlogCategoryMappingRepository : GenericRepository<BlogCategoryMapping>, IBlogCategoryMappingRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public BlogCategoryMappingRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetCategoriesByBlog(int blogId)
        {
            var categories = await _dbContext.BlogCategoryMappings
                .Where(q => q.BlogId == blogId)
                .Select(q => q.Category)
                .ToListAsync();
            return categories;
        }

        public async Task<List<Blog>> GetBlogsByCategory(int categoryId)
        {
            var blogs = await _dbContext.BlogCategoryMappings
                .Where(q => q.CategoryId == categoryId)
                .Select(q => q.Blog)
                .ToListAsync();
            return blogs;
        }

        public async Task<bool> Exists(int blogId, int categoryId)
        {
            return await _dbContext.BlogCategoryMappings.AnyAsync(q => q.BlogId == blogId && q.CategoryId == categoryId);
        }
    }
}
