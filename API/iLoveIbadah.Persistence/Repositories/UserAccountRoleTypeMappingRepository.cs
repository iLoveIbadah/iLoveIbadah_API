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
    public class UserAccountRoleTypeMappingRepository : GenericRepository<UserAccountRoleTypeMapping>, IUserAccountRoleTypeMappingRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public UserAccountRoleTypeMappingRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserAccountRoleTypeMapping>> GetUserAccountRoleTypeMappingsWithDetails()
        {
            var userAccountRoleTypeMappings = await _dbContext.UserAccountRoleTypeMappings
                .Include(p => p.UserAccount)
                .Include(p => p.RoleType)
                .ToListAsync();
            return userAccountRoleTypeMappings;
        }

        public async Task<UserAccountRoleTypeMapping> GetUserAccountRoleTypeMappingWithDetails(int id)
        {
            var userAccountRoleTypeMapping = await _dbContext.UserAccountRoleTypeMappings
                .Include(p => p.UserAccount)
                .Include(p => p.RoleType)
                .FirstOrDefaultAsync(p => p.Id == id);
            return userAccountRoleTypeMapping;
        }
    }
}
