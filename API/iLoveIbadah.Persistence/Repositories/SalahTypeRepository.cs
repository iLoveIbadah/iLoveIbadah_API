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
    public class SalahTypeRepository : GenericRepository<SalahType>, ISalahTypeRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public SalahTypeRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SalahType>> GetSalahTypesWithDetails()
        {
            var salahTypes = await _dbContext.SalahTypes
                .ToListAsync();
            return salahTypes;
        }

        public async Task<SalahType> GetSalahTypeWithDetails(int id)
        {
            var salahType = await _dbContext.SalahTypes
                .FirstOrDefaultAsync(q => q.Id == id);
            return salahType;
        }
    }
}
