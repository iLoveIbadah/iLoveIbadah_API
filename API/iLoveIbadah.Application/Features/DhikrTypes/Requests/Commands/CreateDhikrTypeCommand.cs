using iLoveIbadah.Application.DTOs.DhikrType;
using iLoveIbadah.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.DhikrTypes.Requests.Commands
{
    public class CreateDhikrTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateDhikrTypeDto DhikrTypeDto { get; set; }
    }
}
