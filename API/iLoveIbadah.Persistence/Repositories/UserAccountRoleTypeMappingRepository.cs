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

        public async Task<List<UserAccount>> GetUserAccountsByRole(int roleTypeId)
        {
            var userAccounts = await _dbContext.UserAccountRoleTypeMappings
                .Where(m => m.RoleTypeId == roleTypeId)
                .Include(m => m.UserAccount)
                .Select(m => m.UserAccount)
                .ToListAsync();

            return userAccounts;
        }

        public async Task<List<RoleType>> GetRolesByUserAccount(int userAccountId)
        {
            var roles = await _dbContext.UserAccountRoleTypeMappings
                .Where(m => m.UserAccountId == userAccountId)
                .Include(m => m.RoleType)
                .Select(m => m.RoleType)
                .ToListAsync();

            return roles;
        }

        public async Task<bool> Exists(int userAccountId, int roleTypeId)
        {
            return await _dbContext.UserAccountRoleTypeMappings
                .AnyAsync(q => q.UserAccountId == userAccountId && q.RoleTypeId == roleTypeId);
        }

        //public async Task<List<UserAccountRoleTypeMapping>> GetUserAccountRoleTypeMappingsWithDetails()
        //{
        //    var userAccountRoleTypeMappings = await _dbContext.UserAccountRoleTypeMappings
        //        .Include(p => p.UserAccount)
        //        .Include(p => p.RoleType)
        //        .ToListAsync();
        //    return userAccountRoleTypeMappings;
        //}

        //public async Task<UserAccountRoleTypeMapping> GetUserAccountRoleTypeMappingWithDetails(int id)
        //{
        //    var userAccountRoleTypeMapping = await _dbContext.UserAccountRoleTypeMappings
        //        .Include(p => p.UserAccount)
        //        .Include(p => p.RoleType)
        //        .FirstOrDefaultAsync(p => p.Id == id);
        //    return userAccountRoleTypeMapping;
        //}
    }
}
