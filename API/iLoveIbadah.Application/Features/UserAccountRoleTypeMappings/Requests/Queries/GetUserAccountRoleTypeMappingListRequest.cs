﻿using iLoveIbadah.Application.DTOs.UserAccountRoleTypeMapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserAccountRoleTypeMappings.Requests.Queries
{
    public class GetUserAccountRoleTypeMappingListRequest : IRequest<List<UserAccountRoleTypeMappingListDto>>
    {
        public int UserAccountId { get; set; }
        public int RoleTypeId { get; set; }
    }
}
