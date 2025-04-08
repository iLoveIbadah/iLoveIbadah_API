using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.Features.SalahTypes.Requests.Queries;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.SalahType;

namespace iLoveIbadah.Application.Features.SalahTypes.Handlers.Queries
{
    public class GetSalahTypeListRequestHandler : IRequestHandler<GetSalahTypeListRequest, List<SalahTypeListDto>>
    {
        private readonly ISalahTypeRepository _salahTypeRepository;
        private readonly IMapper _mapper;

        public GetSalahTypeListRequestHandler(ISalahTypeRepository salahTypeRepository, IMapper mapper)
        {
            _salahTypeRepository = salahTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<SalahTypeListDto>> Handle(GetSalahTypeListRequest request, CancellationToken cancellationToken)
        {
            var salahTypes = await _salahTypeRepository.GetAll();
            return _mapper.Map<List<SalahTypeListDto>>(salahTypes);
        }

    }
}
