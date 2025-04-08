using AutoMapper;
using iLoveIbadah.Application.DTOs.UserDhikrActivity;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.Features.UserDhikrActivities.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserDhikrActivities.Handlers.Queries
{
    public class CheckUserDhikrActivityPerformedOnExistsRequestHandler : IRequestHandler<CheckUserDhikrActivityPerformedOnExistsRequest, bool>
    {
        private readonly IUserDhikrActivityRepository _userDhikrActivityRepository;
        private readonly IMapper _mapper;

        public CheckUserDhikrActivityPerformedOnExistsRequestHandler(IUserDhikrActivityRepository userDhikrActivityRepository, IMapper mapper)
        {
            _userDhikrActivityRepository = userDhikrActivityRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CheckUserDhikrActivityPerformedOnExistsRequest request, CancellationToken cancellationToken)
        {
            var userDhikrActivityPerformedOnExists = await _userDhikrActivityRepository.PerformedOnExists(request.UserAccountId, request.PerformedOn, request.DhikrTypeId);
            return userDhikrActivityPerformedOnExists;
        }
    }
}
