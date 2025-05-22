using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.Comment;
using iLoveIbadah.Application.Features.Comments.Requests.Queries;
using MediatR;

namespace iLoveIbadah.Application.Features.Comments.Handlers.Queries
{
    public class GetCommentDetailsRequestHandler : IRequestHandler<GetCommentDetailsRequest, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMapper _mapper;

        public GetCommentDetailsRequestHandler(ICommentRepository commentRepository, IUserAccountRepository userAccountRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userAccountRepository = userAccountRepository;
            _mapper = mapper;
        }

        public async Task<CommentDto> Handle(GetCommentDetailsRequest request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetById(request.Id);
            return _mapper.Map<CommentDto>(comment);
        }
    }
}
