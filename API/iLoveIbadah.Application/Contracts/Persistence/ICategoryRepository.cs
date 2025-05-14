using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetCategoryWithDetails(int id);
        Task<List<Category>> GetCategoriesWithDetails();
        Task<bool> Exists(string fullName);
    }
}
