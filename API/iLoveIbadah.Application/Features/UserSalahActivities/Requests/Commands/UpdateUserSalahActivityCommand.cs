using iLoveIbadah.Application.DTOs.UserSalahActivity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserSalahActivities.Requests.Commands
{
    public class UpdateUserSalahActivityCommand : IRequest<Unit>
    {
        public UpdateUserSalahActivityDto UserSalahActivityDto { get; set; }
    }
}
