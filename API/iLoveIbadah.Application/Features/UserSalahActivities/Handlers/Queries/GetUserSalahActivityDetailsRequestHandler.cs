using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.UserSalahActivity;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.Features.UserSalahActivities.Requests.Queries;

namespace iLoveIbadah.Application.Features.UserSalahActivities.Handlers.Queries
{
    public class GetUserSalahActivityDetailsRequestHandler : IRequestHandler<GetUserSalahActivityDetailsRequest, UserSalahActivityDto>
    {
        private readonly IUserSalahActivityRepository _userSalahActivityRepository;
        private readonly IMapper _mapper;

        public GetUserSalahActivityDetailsRequestHandler(IUserSalahActivityRepository userSalahActivityRepository, IMapper mapper)
        {
            _userSalahActivityRepository = userSalahActivityRepository;
            _mapper = mapper;
        }
        public async Task<UserSalahActivityDto> Handle(GetUserSalahActivityDetailsRequest request, CancellationToken cancellationToken)
        {
            var userSalahActivity = await _userSalahActivityRepository.GetById(request.Id);
            return _mapper.Map<UserSalahActivityDto>(userSalahActivity);
        }
    }
}
