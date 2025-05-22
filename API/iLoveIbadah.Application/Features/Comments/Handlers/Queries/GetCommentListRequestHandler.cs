using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.Comment;
using iLoveIbadah.Application.Features.BlobFiles.Handlers.Queries;
using iLoveIbadah.Application.Features.Comments.Requests.Queries;
using MediatR;

namespace iLoveIbadah.Application.Features.Comments.Handlers.Queries
{
    public class GetCommentListRequestHandler : IRequestHandler<GetCommentListRequest, List<CommentListDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMapper _mapper;

        public GetCommentListRequestHandler(ICommentRepository commentRepository, IUserAccountRepository userAccountRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userAccountRepository = userAccountRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentListDto>> Handle(GetCommentListRequest request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetAll();
            return _mapper.Map<List<CommentListDto>>(comments);
        }
    }
}
