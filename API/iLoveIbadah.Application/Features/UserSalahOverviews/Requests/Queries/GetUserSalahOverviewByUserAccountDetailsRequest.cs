using iLoveIbadah.Application.DTOs.UserSalahOverview;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserSalahOverviews.Requests.Queries
{
    public class GetUserSalahOverviewByUserAccountDetailsRequest : IRequest<UserSalahOverviewDto>
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public int TotalTracked { get; set; }
        public DateTime LastTrackedAt { get; set; }
    }
}
