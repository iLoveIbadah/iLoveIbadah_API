using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface IUserAccountRoleTypeMappingRepository : IGenericRepository<UserAccountRoleTypeMapping>
    {
        Task<List<UserAccount>> GetUserAccountsByRole(int roleTypeId);
        Task<List<RoleType>> GetRolesByUserAccount(int userAccountId);
        Task<bool> Exists(int userAccountId, int roleTypeId);
        //Task<UserAccountRoleTypeMapping> GetUserAccountRoleTypeMappingWithDetails(int id);
        //Task<List<UserAccountRoleTypeMapping>> GetUserAccountRoleTypeMappingsWithDetails();
        //Task<UserAccountRoleTypeMapping> GetUserAccountRoleTypeMappingByUserAccountAndRoleType(int userAccountId, int roleTypeId);
        //Task<List<UserAccountRoleTypeMapping>> GetUserAccountRoleTypeMappingsByUserAccount(int UserAccountId);
        //Task<List<UserAccountRoleTypeMapping>> GetUserAccountRoleTypeMappingsByRoleType(int roleTypeId);
    }
}
