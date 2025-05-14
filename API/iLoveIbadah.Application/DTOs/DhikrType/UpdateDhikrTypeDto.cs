using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.DhikrType
{
    public class UpdateDhikrTypeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? ArabicFullName { get; set; }
    }
}
