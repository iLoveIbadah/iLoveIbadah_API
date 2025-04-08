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
    public class UserSalahOverviewRepository : GenericRepository<UserSalahOverview>, IUserSalahOverviewRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public UserSalahOverviewRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserSalahOverview>> GetUserSalahOverviewsWithDetails()
        {
            var userSalahOverviews = await _dbContext.UserSalahOverviews
                .Include(q => q.UserAccount)
                .ToListAsync();
            return userSalahOverviews;
        }

        public async Task<UserSalahOverview> GetUserSalahOverviewWithDetails(int id)
        {
            var userSalahOverview = await _dbContext.UserSalahOverviews
                .Include(q => q.UserAccount)
                .FirstOrDefaultAsync(q => q.Id == id);
            return userSalahOverview;
        }

        public async Task<UserSalahOverview> GetUserSalahOverviewByUserAccountWithDetails(int userAccountId)
        {
            var userSalahOverview = await _dbContext.UserSalahOverviews
                .Include(q => q.UserAccount)
                .FirstOrDefaultAsync(q => q.UserAccountId == userAccountId);
            return userSalahOverview;
        }
    }
}
