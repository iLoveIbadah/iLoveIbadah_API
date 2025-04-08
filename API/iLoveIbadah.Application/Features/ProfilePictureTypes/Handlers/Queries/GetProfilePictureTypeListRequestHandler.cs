using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.ProfilePictureType;
using iLoveIbadah.Application.Features.ProfilePictureTypes.Requests.Queries;
using iLoveIbadah.Application.Contracts.Persistence;

namespace iLoveIbadah.Application.Features.ProfilePictureTypes.Handlers.Queries
{
    public class GetProfilePictureTypeListRequestHandler : IRequestHandler<GetProfilePictureTypeListRequest, List<ProfilePictureTypeListDto>>
    {
        private readonly IProfilePictureTypeRepository _profilePictureTypeRepository;
        private readonly IMapper _mapper;

        public GetProfilePictureTypeListRequestHandler(IProfilePictureTypeRepository profilePictureTypeRepository, IMapper mapper)
        {
            _profilePictureTypeRepository = profilePictureTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<ProfilePictureTypeListDto>> Handle(GetProfilePictureTypeListRequest request, CancellationToken cancellationToken)
        {
            var profilePictureTypes = await _profilePictureTypeRepository.GetAll();
            return _mapper.Map<List<ProfilePictureTypeListDto>>(profilePictureTypes);
        }

    }
}
