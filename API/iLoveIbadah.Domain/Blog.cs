using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Domain
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        [ForeignKey("BlobFile")]
        public int? BlobFileId { get; set; } // Thumbnail!!!
        public BlobFile? BlobFile { get; set; }
        public int TotalViews { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
