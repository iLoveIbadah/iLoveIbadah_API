using iLoveIbadah.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.Blog;

namespace iLoveIbadah.Application.Features.Blogs.Requests.Queries
{
    public class GetBlogListRequest : IRequest<List<BlogListDto>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? BlobFileId { get; set; } // Thumbnail!!!
        public int TotalViews { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
