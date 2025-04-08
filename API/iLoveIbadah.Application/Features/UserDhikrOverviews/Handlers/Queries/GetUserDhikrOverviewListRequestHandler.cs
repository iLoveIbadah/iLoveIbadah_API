using AutoMapper;
using iLoveIbadah.Domain;
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
    public class GetUserDhikrOverviewListRequestHandler : IRequestHandler<GetUserDhikrOverviewListRequest, List<UserDhikrOverviewListDto>>
    {
        private readonly IUserDhikrOverviewRepository _userDhikrOverviewRepository;
        private readonly IMapper _mapper;

        public GetUserDhikrOverviewListRequestHandler(IUserDhikrOverviewRepository userDhikrOverviewRepository, IMapper mapper)
        {
            _userDhikrOverviewRepository = userDhikrOverviewRepository;
            _mapper = mapper;
        }
        public async Task<List<UserDhikrOverviewListDto>> Handle(GetUserDhikrOverviewListRequest request, CancellationToken cancellationToken)
        {
            var userDhikrOverview = await _userDhikrOverviewRepository.GetAll();
            return _mapper.Map<List<UserDhikrOverviewListDto>>(userDhikrOverview);
        }
    }
}
