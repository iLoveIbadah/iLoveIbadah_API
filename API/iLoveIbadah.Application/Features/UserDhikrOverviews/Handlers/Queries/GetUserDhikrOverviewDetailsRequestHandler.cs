using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.UserDhikrOverview;
using iLoveIbadah.Application.Features.UserDhikrOverviews.Requests.Queries;

namespace iLoveIbadah.Application.Features.UserDhikrOverviews.Handlers.Queries
{
    //public class GetUserDhikrOverviewDetailsRequestHandler IRequestHandler<GetUserDhikrActivityDetailsRequest, UserDhikrActivityDto>
    public class GetUserDhikrOverviewDetailsRequestHandler : IRequestHandler<GetUserDhikrOverviewDetailsRequest, UserDhikrOverviewDto>
    {
        private readonly IUserDhikrOverviewRepository _userDhikrOverviewRepository;
        private readonly IMapper _mapper;

        public GetUserDhikrOverviewDetailsRequestHandler(IUserDhikrOverviewRepository userDhikrOverviewRepository, IMapper mapper)
        {
            _userDhikrOverviewRepository = userDhikrOverviewRepository;
            _mapper = mapper;
        }
        public async Task<UserDhikrOverviewDto> Handle(GetUserDhikrOverviewDetailsRequest request, CancellationToken cancellationToken)
        {
            var userDhikrOverview = await _userDhikrOverviewRepository.GetById(request.Id);
            return _mapper.Map<UserDhikrOverviewDto>(userDhikrOverview);
        }
    }
}
