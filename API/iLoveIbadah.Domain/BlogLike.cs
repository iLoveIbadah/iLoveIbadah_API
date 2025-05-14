using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Domain
{
    public class BlogLike
    {
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; } // Navigation property
        [ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; } // Navigation property
    }
}
