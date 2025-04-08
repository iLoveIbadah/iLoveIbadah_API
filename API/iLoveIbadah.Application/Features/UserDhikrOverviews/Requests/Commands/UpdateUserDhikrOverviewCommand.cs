using iLoveIbadah.Application.DTOs.UserDhikrOverview;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserDhikrOverviews.Requests.Commands
{
    public class UpdateUserDhikrOverviewCommand : IRequest<Unit>
    {
        public UpdateUserDhikrOverviewDto UserDhikrOverviewDto { get; set; }
    }
}
