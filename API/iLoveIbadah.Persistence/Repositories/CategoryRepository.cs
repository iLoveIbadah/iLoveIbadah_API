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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;

        public CategoryRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetCategoriesWithDetails()
        {
            var categories = await _dbContext.Categories
                .ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryWithDetails(int id)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(q => q.Id == id);
            return category;
        }

        public async Task<bool> Exists(string fullName)
        {
            return await _dbContext.Categories.AnyAsync(q => q.FullName == fullName);
            //var category = await _dbContext.Categories
            //    .FirstOrDefaultAsync(q => q.Name == name);
            //return category != null;
        }
    }
}
