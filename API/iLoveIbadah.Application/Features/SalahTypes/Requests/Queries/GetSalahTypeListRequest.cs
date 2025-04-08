using iLoveIbadah.Application.DTOs.SalahType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.SalahTypes.Requests.Queries
{
    public class GetSalahTypeListRequest : IRequest<List<SalahTypeListDto>>
    {
        public string FullName { get; set; }
        public int CreatedBy { get; set; }
    }
}
