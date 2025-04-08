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
    public class BlobFileRepository : GenericRepository<BlobFile>, IBlobFileRepository
    {
        private readonly iLoveIbadahDbContext _dbContext;
        public BlobFileRepository(iLoveIbadahDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BlobFile>> GetBlobFilesWithDetails()
        {
            var blobFiles = await _dbContext.BlobFiles
                .ToListAsync();
            return blobFiles;
        }

        public async Task<BlobFile> GetBlobFileWithDetails(int id)
        {
            var blobFile = await _dbContext.BlobFiles
                .FirstOrDefaultAsync(Queryable => Queryable.Id == id);
            return blobFile;
        }
    }
}
