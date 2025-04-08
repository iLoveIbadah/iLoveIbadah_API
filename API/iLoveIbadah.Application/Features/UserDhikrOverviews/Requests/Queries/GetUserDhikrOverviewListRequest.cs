using iLoveIbadah.Application.DTOs.UserDhikrOverview;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.UserDhikrOverviews.Requests.Queries
{
    public class GetUserDhikrOverviewListRequest : IRequest<List<UserDhikrOverviewListDto>>
    {
        public int UserAccountId { get; set; }
        public int DhikrTypeId { get; set; }
        public int TotalPerformed { get; set; }
    }
}
