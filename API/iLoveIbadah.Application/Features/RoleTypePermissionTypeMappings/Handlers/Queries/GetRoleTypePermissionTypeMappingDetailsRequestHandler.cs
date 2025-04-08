using AutoMapper;
using iLoveIbadah.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.Features.RoleTypePermissionTypeMappings.Requests.Queries;
using iLoveIbadah.Application.DTOs.RoleTypePermissionTypeMapping;

namespace iLoveIbadah.Application.Features.RoleTypePermissionTypeMappings.Handlers.Queries
{
    public class GetRoleTypePermissionTypeMappingDetailsRequestHandler : IRequestHandler<GetRoleTypePermissionTypeMappingDetailsRequest, RoleTypePermissionTypeMappingDto>
    {
        private readonly IRoleTypePermissionTypeMappingRepository _roleTypePermissionTypeMappingRepository;
        private readonly IMapper _mapper;

        public GetRoleTypePermissionTypeMappingDetailsRequestHandler(IRoleTypePermissionTypeMappingRepository roleTypePermissionTypeMappingRepository, IMapper mapper)
        {
            _roleTypePermissionTypeMappingRepository = roleTypePermissionTypeMappingRepository;
            _mapper = mapper;
        }
        public async Task<RoleTypePermissionTypeMappingDto> Handle(GetRoleTypePermissionTypeMappingDetailsRequest request, CancellationToken cancellationToken)
        {
            var roleTypePermissionTypeMapping = await _roleTypePermissionTypeMappingRepository.GetById(request.Id);
            return _mapper.Map<RoleTypePermissionTypeMappingDto>(roleTypePermissionTypeMapping);
        }
    }
}
