using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        Task<Blog> GetBlogWithDetails(int id);
        Task<List<Blog>> GetBlogsWithDetails();
    }
}
