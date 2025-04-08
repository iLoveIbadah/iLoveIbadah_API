using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Identity.Models
{
    public class ApplicationUserToken : IdentityUserToken<int>
    {
        public int Id { get; set; }
    }
}
