using AutoMapper;
using iLoveIbadah.Application.DTOs.UserAccount;
using iLoveIbadah.Application.Features.UserAccounts.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Features.UserDhikrActivities.Requests.Queries;
using iLoveIbadah.Application.DTOs.UserDhikrActivity;
using iLoveIbadah.Application.Contracts.Persistence;

namespace iLoveIbadah.Application.Features.UserDhikrActivities.Handlers.Queries
{
    // Backend Handler of Request to get Details of User Activity
    public class GetUserDhikrActivityDetailsRequestHandler : IRequestHandler<GetUserDhikrActivityDetailsRequest, UserDhikrActivityDto>
    {
        private readonly IUserDhikrActivityRepository _userDhikrActivityRepository;
        private readonly IMapper _mapper;

        public GetUserDhikrActivityDetailsRequestHandler(IUserDhikrActivityRepository userDhikrActivityRepository, IMapper mapper)
        {
            _userDhikrActivityRepository = userDhikrActivityRepository;
            _mapper = mapper;
        }
        public async Task<UserDhikrActivityDto> Handle(GetUserDhikrActivityDetailsRequest request, CancellationToken cancellationToken)
        {
            var userDhikrActivity = await _userDhikrActivityRepository.GetById(request.Id);
            return _mapper.Map<UserDhikrActivityDto>(userDhikrActivity);
        }
    }
}
