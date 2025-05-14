using iLoveIbadah.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Persistence
{
    public interface IBlogTagMappingRepository : IGenericRepository<BlogTagMapping>
    {
        Task<List<Tag>> GetTagsByBlog(int blogId);
        Task<List<Blog>> GetBlogsByTag(int tagId);
        Task<bool> Exists(int blogId, int tagId);
    }
}
