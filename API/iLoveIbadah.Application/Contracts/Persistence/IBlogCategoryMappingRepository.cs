using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface IBlogCategoryMappingRepository : IGenericRepository<BlogCategoryMapping>
    {
        Task<List<Category>> GetCategoriesByBlog(int blogId);
        Task<List<Blog>> GetBlogsByCategory(int categoryId);
        Task<bool> Exists(int blogdId, int categoryId);
    }
}
