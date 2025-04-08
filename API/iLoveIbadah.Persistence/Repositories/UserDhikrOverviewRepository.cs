using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Domain;
using iLoveIbadah.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Persistence.Repositories
{
    public class UserDhikrOverviewRepository : GenericRepository<UserDhikrOverview>, IUserDhikrOverviewRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public UserDhikrOverviewRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserDhikrOverview>> GetUserDhikrOverviewsWithDetails()
        {
            var userDhikrOverviews = await _dbContext.UserDhikrOverviews
                .Include(q => q.UserAccount)
                .ToListAsync();
            return userDhikrOverviews;
        }

        public async Task<UserDhikrOverview> GetUserDhikrOverviewWithDetails(int id)
        {
            var userDhikrOverview = await _dbContext.UserDhikrOverviews
                .Include(q => q.UserAccount)
                .FirstOrDefaultAsync(q => q.Id == id);
            return userDhikrOverview;
        }

        public async Task<UserDhikrOverview> GetUserDhikrOverviewByUserAccountWithDetails(int userAccountId)
        {
            var userDhikrOverview = await _dbContext.UserDhikrOverviews
                .Include(q => q.UserAccount)
                .FirstOrDefaultAsync(q => q.UserAccountId == userAccountId);
            return userDhikrOverview;
        }
    }
}
