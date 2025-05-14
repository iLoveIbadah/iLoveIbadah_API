using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<Tag> GetTagWithDetails(int id);
        Task<List<Tag>> GetTagsWithDetails();
        Task<bool> Exists(string fullName);
    }
}
