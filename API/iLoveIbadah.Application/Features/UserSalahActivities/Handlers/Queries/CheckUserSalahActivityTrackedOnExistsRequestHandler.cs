using AutoMapper;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.Features.UserSalahActivities.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserSalahActivities.Handlers.Queries
{
    public class CheckUserSalahActivityTrackedOnExistsRequestHandler : IRequestHandler<CheckUserSalahActivityTrackedOnExistsRequest, bool>
    {
        private readonly IUserSalahActivityRepository _userSalahActivityRepository;
        private readonly IMapper _mapper;

        public CheckUserSalahActivityTrackedOnExistsRequestHandler(IUserSalahActivityRepository userSalahActivityRepository, IMapper mapper)
        {
            _userSalahActivityRepository = userSalahActivityRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CheckUserSalahActivityTrackedOnExistsRequest request, CancellationToken cancellationToken)
        {
            var userSalahActivityPerformedOnExists = await _userSalahActivityRepository.TrackedOnExists(request.UserAccountId.Value, request.TrackedOn, request.SalahTypeId);
            return userSalahActivityPerformedOnExists;
        }
    }
}
