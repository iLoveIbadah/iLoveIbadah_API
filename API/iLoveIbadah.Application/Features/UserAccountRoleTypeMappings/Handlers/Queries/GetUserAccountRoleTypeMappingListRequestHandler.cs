using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Features.UserAccountRoleTypeMappings.Requests.Queries;
using iLoveIbadah.Application.DTOs.UserAccountRoleTypeMapping;
using iLoveIbadah.Application.Contracts.Persistence;

namespace iLoveIbadah.Application.Features.UserAccountRoleTypeMappings.Handlers.Queries
{
    public class GetUserAccountRoleTypeMappingListRequestHandler : IRequestHandler<GetUserAccountRoleTypeMappingListRequest, List<UserAccountRoleTypeMappingListDto>>
    {
        private readonly IUserAccountRoleTypeMappingRepository _userAccountRoleTypeMappingRepository;
        private readonly IMapper _mapper;

        public GetUserAccountRoleTypeMappingListRequestHandler(IUserAccountRoleTypeMappingRepository userAccountRoleTypeMappingRepository, IMapper mapper)
        {
            _userAccountRoleTypeMappingRepository = userAccountRoleTypeMappingRepository;
            _mapper = mapper;
        }
        public async Task<List<UserAccountRoleTypeMappingListDto>> Handle(GetUserAccountRoleTypeMappingListRequest request, CancellationToken cancellationToken)
        {
            var userAccountRoleTypeMappings = await _userAccountRoleTypeMappingRepository.GetAll();
            return _mapper.Map<List<UserAccountRoleTypeMappingListDto>>(userAccountRoleTypeMappings);
        }
    }
}
