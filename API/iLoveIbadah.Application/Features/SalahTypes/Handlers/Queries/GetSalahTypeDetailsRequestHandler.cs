using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Features.SalahTypes.Requests.Queries;
using iLoveIbadah.Application.DTOs.SalahType;
using iLoveIbadah.Application.Contracts.Persistence;

namespace iLoveIbadah.Application.Features.SalahTypes.Handlers.Queries
{
    public class GetSalahTypeDetailsRequestHandler : IRequestHandler<GetSalahTypeDetailsRequest, SalahTypeDto>
    {
        private readonly ISalahTypeRepository _salahTypeRepository;
        private readonly IMapper _mapper;

        public GetSalahTypeDetailsRequestHandler(ISalahTypeRepository salahTypeRepository, IMapper mapper)
        {
            _salahTypeRepository = salahTypeRepository;
            _mapper = mapper;
        }
        public async Task<SalahTypeDto> Handle(GetSalahTypeDetailsRequest request, CancellationToken cancellationToken)
        {
            var salahType = await _salahTypeRepository.GetById(request.Id);
            return _mapper.Map<SalahTypeDto>(salahType);
        }
    }
}
