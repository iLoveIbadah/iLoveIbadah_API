using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface IPermissionTypeRepository : IGenericRepository<PermissionType>
    {
        Task<PermissionType> GetPermissionTypeWithDetails(int id);
        Task<List<PermissionType>> GetPermissionTypesWithDetails();
    }
}
