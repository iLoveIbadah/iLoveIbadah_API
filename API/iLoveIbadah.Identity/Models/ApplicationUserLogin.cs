using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Identity.Models
{
    public class ApplicationUserLogin : IdentityUserLogin<int>
    {
        public int Id { get; set; }
    }
}
