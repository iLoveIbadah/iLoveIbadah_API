using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.PermissionType;
using iLoveIbadah.Application.Features.PermissionTypes.Requests.Queries;
using iLoveIbadah.Application.Contracts.Persistence;

namespace iLoveIbadah.Application.Features.PermissionTypes.Handlers.Queries
{
    public class GetPermissionTypeDetailsRequestHandler : IRequestHandler<GetPermissionTypeDetailsRequest, PermissionTypeDto>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IMapper _mapper;

        public GetPermissionTypeDetailsRequestHandler(IPermissionTypeRepository permissionTypeRepository, IMapper mapper)
        {
            _permissionTypeRepository = permissionTypeRepository;
            _mapper = mapper;
        }
        public async Task<PermissionTypeDto> Handle(GetPermissionTypeDetailsRequest request, CancellationToken cancellationToken)
        {
            var permissionType = await _permissionTypeRepository.GetById(request.Id);
            return _mapper.Map<PermissionTypeDto>(permissionType);
        }
    }
}
