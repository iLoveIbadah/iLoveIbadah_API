using iLoveIbadah.Application.DTOs.UserDhikrActivity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserDhikrActivities.Requests.Commands
{
    public class UpdateUserDhikrActivityCommand : IRequest<Unit>
    {
        public UpdateUserDhikrActivityDto UserDhikrActivityDto { get; set; }
    }
}
