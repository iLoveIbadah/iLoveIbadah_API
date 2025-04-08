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
    public class ProfilePictureTypeRepository : GenericRepository<ProfilePictureType>, IProfilePictureTypeRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public ProfilePictureTypeRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProfilePictureType>> GetProfilePictureTypesWithDetails()
        {
            var profilePictureTypes = await _dbContext.ProfilePictureTypes
                .ToListAsync();
            return profilePictureTypes;
        }

        public async Task<ProfilePictureType> GetProfilePictureTypeWithDetails(int id)
        {
            var profilePictureType = await _dbContext.ProfilePictureTypes
                .FirstOrDefaultAsync(q => q.Id == id);
            return profilePictureType;
        }
    }
}
