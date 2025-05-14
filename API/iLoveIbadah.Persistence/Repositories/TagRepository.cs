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
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;

        public TagRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tag>> GetTagsWithDetails()
        {
            var tags = await _dbContext.Tags
                .ToListAsync();
            return tags;
        }

        public async Task<Tag> GetTagWithDetails(int id)
        {
            var tag = await _dbContext.Tags
                .FirstOrDefaultAsync(q => q.Id == id);
            return tag;
        }

        public async Task<bool> Exists(string fullName)
        {
            return await _dbContext.Tags.AnyAsync(q => q.FullName == fullName);
            //var tag = await _dbContext.Tags
            //    .FirstOrDefaultAsync(q => q.Name == name);
            //return tag != null;
        }
    }
}
