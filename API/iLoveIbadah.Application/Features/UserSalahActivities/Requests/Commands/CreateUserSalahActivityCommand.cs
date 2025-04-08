using iLoveIbadah.Application.DTOs.UserSalahActivity;
using iLoveIbadah.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserSalahActivities.Requests.Commands
{
    public class CreateUserSalahActivityCommand : IRequest<BaseCommandResponse>
    {
        public CreateUserSalahActivityDto UserSalahActivityDto { get; set; }
    }
}
