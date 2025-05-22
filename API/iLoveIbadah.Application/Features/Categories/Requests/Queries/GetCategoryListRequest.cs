using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.Category;
using MediatR;

namespace iLoveIbadah.Application.Features.Categories.Requests.Queries
{
    public class GetCategoryListRequest : IRequest<List<CategoryListDto>>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
