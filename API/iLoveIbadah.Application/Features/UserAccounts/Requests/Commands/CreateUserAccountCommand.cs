using iLoveIbadah.Application.DTOs.UserAccount;
using iLoveIbadah.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserAccounts.Requests.Commands
{
    public class CreateUserAccountCommand : IRequest<BaseCommandResponse>
    {
        public CreateUserAccountDto UserAccountDto { get; set; }
    }
}
