using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Blog
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public int BlobFileId { get; set; } // Thumbnail!!!
        public int TotalViews { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
