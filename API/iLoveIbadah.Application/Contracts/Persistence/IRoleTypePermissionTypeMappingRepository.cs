using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{

    public interface IRoleTypePermissionTypeMappingRepository : IGenericRepository<RoleTypePermissionTypeMapping>
    {
        Task<List<RoleType>> GetRolesByPemission(int permissionTypeId); // get all roles that have said permission
        Task<List<PermissionType>> GetPermissionsByRole(int roleTypeId); // get all permissions that said role has
        Task<bool> Exists(int roleTypeId, int permissionTypeId);

        //Task<RoleTypePermissionTypeMapping> GetRoleTypePermissionTypeMappingWithDetails(int id);
        //Task<List<RoleTypePermissionTypeMapping>> GetRoleTypePermissionTypeMappingsWithDetails();
        //Task<RoleTypePermissionTypeMapping> GetRoleTypePermissionTypeMappingByRoleTypeAndPermissionType(int roleTypeId, int permissionTypeId);
        //Task<List<RoleTypePermissionTypeMapping>> GetRoleTypePermissionTypeMappingsByRoleType(int roleTypeId);
        //Task<List<RoleTypePermissionTypeMapping>> GetRoleTypePermissionTypeMappingsByPermissionType(int permissionTypeId);
    }
}
