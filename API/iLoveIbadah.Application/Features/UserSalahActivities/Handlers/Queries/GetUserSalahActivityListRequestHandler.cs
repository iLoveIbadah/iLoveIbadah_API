using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Features.UserSalahActivities.Requests.Queries;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.UserSalahActivity;

namespace iLoveIbadah.Application.Features.UserSalahActivities.Handlers.Queries
{
    public class GetUserSalahActivityListRequestHandler : IRequestHandler<GetUserSalahActivityListRequest, List<UserSalahActivityListDto>>
    {
        private readonly IUserSalahActivityRepository _userSalahActivityRepository;
        private readonly IMapper _mapper;

        public GetUserSalahActivityListRequestHandler(IUserSalahActivityRepository userSalahActivityRepository, IMapper mapper)
        {
            _userSalahActivityRepository = userSalahActivityRepository;
            _mapper = mapper;
        }
        public async Task<List<UserSalahActivityListDto>> Handle(GetUserSalahActivityListRequest request, CancellationToken cancellationToken)
        {
            var userSalahActivity = await _userSalahActivityRepository.GetAll();
            return _mapper.Map<List<UserSalahActivityListDto>>(userSalahActivity);
        }
    }
}
