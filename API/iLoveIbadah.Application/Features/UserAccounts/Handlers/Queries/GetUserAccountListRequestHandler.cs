using AutoMapper;
using iLoveIbadah.Application.Features.DhikrTypes.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.UserAccount;
using iLoveIbadah.Application.Features.UserAccounts.Requests.Queries;

namespace iLoveIbadah.Application.Features.UserAccounts.Handlers.Queries
{
    // Backend Handler of Request to get a List of User Accounts
    public class GetUserAccountListRequestHandler : IRequestHandler<GetUserAccountListRequest, List<UserAccountListDto>>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMapper _mapper;

        public GetUserAccountListRequestHandler(IUserAccountRepository userAccountRepository, IMapper mapper)
        {
            _userAccountRepository = userAccountRepository;
            _mapper = mapper;
        }
        public async Task<List<UserAccountListDto>> Handle(GetUserAccountListRequest request, CancellationToken cancellationToken)
        {
            var userAccounts = await _userAccountRepository.GetAll();
            return _mapper.Map<List<UserAccountListDto>>(userAccounts);
        }
    }
}
