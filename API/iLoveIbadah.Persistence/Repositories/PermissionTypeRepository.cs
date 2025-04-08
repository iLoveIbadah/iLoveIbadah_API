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
    public class PermissionTypeRepository : GenericRepository<PermissionType>, IPermissionTypeRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public PermissionTypeRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PermissionType>> GetPermissionTypesWithDetails()
        {
            var permissionTypes = await _dbContext.PermissionTypes
                .ToListAsync();
            return permissionTypes;
        }

        public async Task<PermissionType> GetPermissionTypeWithDetails(int id)
        {
            var permissionType = await _dbContext.PermissionTypes
                .FirstOrDefaultAsync(q => q.Id == id);
            return permissionType;

        }
    }
}
