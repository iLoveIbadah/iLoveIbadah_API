using AutoMapper;
using iLoveIbadah.Application.DTOs.PermissionType;
using iLoveIbadah.Application.Features.PermissionTypes.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Features.RoleTypes.Requests.Queries;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.RoleType;

namespace iLoveIbadah.Application.Features.RoleTypes.Handlers.Queries
{
    public class GetRoleTypeDetailsRequestHandler : IRequestHandler<GetRoleTypeDetailsRequest, RoleTypeDto>
    {
        private readonly IRoleTypeRepository _roleTypeRepository;
        private readonly IMapper _mapper;

        public GetRoleTypeDetailsRequestHandler(IRoleTypeRepository roleTypeRepository, IMapper mapper)
        {
            _roleTypeRepository = roleTypeRepository;
            _mapper = mapper;
        }
        public async Task<RoleTypeDto> Handle(GetRoleTypeDetailsRequest request, CancellationToken cancellationToken)
        {
            var roleType = await _roleTypeRepository.GetById(request.Id);
            return _mapper.Map<RoleTypeDto>(roleType);
        }
    }
}
