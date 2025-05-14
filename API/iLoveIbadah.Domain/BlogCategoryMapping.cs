using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Domain
{
    public class BlogCategoryMapping
    {
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; } // Navigation property
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } // Navigation property
    }
}
