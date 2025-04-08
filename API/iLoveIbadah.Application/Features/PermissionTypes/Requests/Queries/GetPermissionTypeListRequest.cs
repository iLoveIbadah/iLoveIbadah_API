using iLoveIbadah.Application.DTOs.PermissionType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.PermissionTypes.Requests.Queries
{
    public class GetPermissionTypeListRequest : IRequest<List<PermissionTypeListDto>>
    {
        public string FullName { get; set; }
        public string Details { get; set; }
    }
}
