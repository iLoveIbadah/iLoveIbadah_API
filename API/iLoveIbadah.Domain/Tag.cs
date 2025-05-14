using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Domain
{
    public class Tag
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        //[ForeignKey("Category")]
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
