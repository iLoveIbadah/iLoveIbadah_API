using iLoveIbadah.Application.DTOs.BlobFile;
using iLoveIbadah.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Features.BlobFiles.Requests.Commands
{
    public class CreateBlobFileCommand : IRequest<BaseCommandResponse>
    {
        public CreateBlobFileDto BlobFileDto { get; set; }
    }
}
