using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Domain
{
    public class BlogTagMapping
    {
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; } // Navigation property
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; } // Navigation property
    }
}
