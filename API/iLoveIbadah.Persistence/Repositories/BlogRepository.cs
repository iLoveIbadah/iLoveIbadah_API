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
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;

        public BlogRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Blog>> GetBlogsWithDetails()
        {
            var blogs = await _dbContext.Blogs
                .ToListAsync();
            return blogs;
        }

        public async Task<Blog> GetBlogWithDetails(int id)
        {
            var blog = await _dbContext.Blogs
                .FirstOrDefaultAsync(q => q.Id == id);
            return blog;
        }
    }
}
