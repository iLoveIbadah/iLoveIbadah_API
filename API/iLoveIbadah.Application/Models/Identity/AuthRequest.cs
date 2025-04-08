using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Models.Identity
{
    public class AuthRequest
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
