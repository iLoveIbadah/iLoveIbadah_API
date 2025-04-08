using AutoMapper;
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
    public class GetRoleTypePermissionTypeMappingListRequestHandler : IRequestHandler<GetRoleTypePermissionTypeMappingListRequest, List<RoleTypePermissionTypeMappingListDto>>
    {
        private readonly IRoleTypePermissionTypeMappingRepository _roleTypePermissionTypeMappingRepository;
        private readonly IMapper _mapper;

        public GetRoleTypePermissionTypeMappingListRequestHandler(IRoleTypePermissionTypeMappingRepository roleTypePermissionTypeMappingRepository, IMapper mapper)
        {
            _roleTypePermissionTypeMappingRepository = roleTypePermissionTypeMappingRepository;
            _mapper = mapper;
        }
        public async Task<List<RoleTypePermissionTypeMappingListDto>> Handle(GetRoleTypePermissionTypeMappingListRequest request, CancellationToken cancellationToken)
        {
            var roleTypesPermissionTypeMappings = await _roleTypePermissionTypeMappingRepository.GetAll();
            return _mapper.Map<List<RoleTypePermissionTypeMappingListDto>>(roleTypesPermissionTypeMappings);
        }
    }
}
