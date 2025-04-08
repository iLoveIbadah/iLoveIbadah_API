using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.UserSalahOverview;
using iLoveIbadah.Application.Features.UserSalahOverviews.Requests.Queries;

namespace iLoveIbadah.Application.Features.UserSalahOverviews.Handlers.Queries
{
    public class GetUserSalahOverviewDetailsRequestHandler : IRequestHandler<GetUserSalahOverviewDetailsRequest, UserSalahOverviewDto>
    {
        private readonly IUserSalahOverviewRepository _userSalahOverviewRepository;
        private readonly IMapper _mapper;

        public GetUserSalahOverviewDetailsRequestHandler(IUserSalahOverviewRepository userSalahOverviewRepository, IMapper mapper)
        {
            _userSalahOverviewRepository = userSalahOverviewRepository;
            _mapper = mapper;
        }
        public async Task<UserSalahOverviewDto> Handle(GetUserSalahOverviewDetailsRequest request, CancellationToken cancellationToken)
        {
            var userSalahOverview = await _userSalahOverviewRepository.GetById(request.Id);
            return _mapper.Map<UserSalahOverviewDto>(userSalahOverview);
        }
    }
}
