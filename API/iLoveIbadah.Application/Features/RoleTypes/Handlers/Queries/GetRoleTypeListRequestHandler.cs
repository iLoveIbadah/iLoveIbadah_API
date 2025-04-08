using AutoMapper;
using iLoveIbadah.Application.DTOs.PermissionType;
using iLoveIbadah.Application.Features.PermissionTypes.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.Features.RoleTypes.Requests.Queries;
using iLoveIbadah.Application.DTOs.RoleType;

namespace iLoveIbadah.Application.Features.RoleTypes.Handlers.Queries
{
    public class GetRoleTypeListRequestHandler : IRequestHandler<GetRoleTypeListRequest, List<RoleTypeListDto>>
    {
        private readonly IRoleTypeRepository _roleTypeRepository;
        private readonly IMapper _mapper;

        public GetRoleTypeListRequestHandler(IRoleTypeRepository roleTypeRepository, IMapper mapper)
        {
            _roleTypeRepository = roleTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<RoleTypeListDto>> Handle(GetRoleTypeListRequest request, CancellationToken cancellationToken)
        {
            var roleTypes = await _roleTypeRepository.GetAll();
            return _mapper.Map<List<RoleTypeListDto>>(roleTypes);
        }
    }
}
