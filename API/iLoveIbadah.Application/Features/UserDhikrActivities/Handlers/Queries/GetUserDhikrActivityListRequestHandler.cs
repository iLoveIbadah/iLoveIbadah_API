using AutoMapper;
using iLoveIbadah.Application.DTOs.UserAccount;
using iLoveIbadah.Application.Features.UserAccounts.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.UserDhikrActivity;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.Features.UserDhikrActivities.Requests.Queries;

namespace iLoveIbadah.Application.Features.UserDhikrActivities.Handlers.Queries
{
    public class GetUserDhikrActivityListRequestHandler : IRequestHandler<GetUserDhikrActivityListRequest, List<UserDhikrActivityListDto>>
    {
        private readonly IUserDhikrActivityRepository _userDhikrActivityRepository;
        private readonly IMapper _mapper;

        public GetUserDhikrActivityListRequestHandler(IUserDhikrActivityRepository userDhikrActivityRepository, IMapper mapper)
        {
            _userDhikrActivityRepository = userDhikrActivityRepository;
            _mapper = mapper;
        }
        public async Task<List<UserDhikrActivityListDto>> Handle(GetUserDhikrActivityListRequest request, CancellationToken cancellationToken)
        {
            var userDhikrActivity = await _userDhikrActivityRepository.GetAll();
            return _mapper.Map<List<UserDhikrActivityListDto>>(userDhikrActivity);
        }
    }
}
