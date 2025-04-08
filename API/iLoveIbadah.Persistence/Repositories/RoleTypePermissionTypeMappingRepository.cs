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
    public class RoleTypePermissionTypeMappingRepository : GenericRepository<RoleTypePermissionTypeMapping>, IRoleTypePermissionTypeMappingRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public RoleTypePermissionTypeMappingRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //public Task<RoleTypePermissionTypeMapping> GetRoleTypePermissionTypeMappingByRoleTypeAndPermissionType(int roleTypeId, int permissionTypeId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<RoleTypePermissionTypeMapping>> GetRoleTypePermissionTypeMappingsByPermissionType(int permissionTypeId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<RoleTypePermissionTypeMapping>> GetRoleTypePermissionTypeMappingsByRoleType(int roleTypeId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<RoleTypePermissionTypeMapping>> GetRoleTypePermissionTypeMappingsWithDetails()
        {
            var roleTypePermissionTypeMappings = await _dbContext.RoleTypePermissionTypeMappings
                .Include(q => q.RoleType)
                .Include(q => q.PermissionType)
                .ToListAsync();
            return roleTypePermissionTypeMappings;
        }

        public async Task<RoleTypePermissionTypeMapping> GetRoleTypePermissionTypeMappingWithDetails(int id)
        {
            var roleTypePermissionTypeMapping = await _dbContext.RoleTypePermissionTypeMappings
                .Include(q => q.RoleType)
                .Include(q => q.PermissionType)
                .FirstOrDefaultAsync(q => q.Id == id);
            return roleTypePermissionTypeMapping;
        }
    }
}
