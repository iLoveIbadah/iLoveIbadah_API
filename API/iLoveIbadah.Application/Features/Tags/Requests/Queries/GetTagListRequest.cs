using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.Tag;
using MediatR;

namespace iLoveIbadah.Application.Features.Tags.Requests.Queries
{
    public class GetTagListRequest : IRequest<List<TagListDto>>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
