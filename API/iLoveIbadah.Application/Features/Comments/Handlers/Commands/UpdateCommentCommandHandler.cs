using AutoMapper;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.Exceptions;
using iLoveIbadah.Application.Features.Comments.Requests.Commands;
using iLoveIbadah.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Application.DTOs.Comment.Validators;

namespace iLoveIbadah.Application.Features.Comments.Handlers.Commands
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Unit>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository, IUserAccountRepository userAccountRepository, IBlogRepository blogRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userAccountRepository = userAccountRepository;
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            if (request.CommentDto.Content == null && request.CommentDto.Id == null)
            {
                return Unit.Value;
            }

            var validation = new UpdateCommentDtoValidator(_commentRepository, _userAccountRepository);
            var validationResult = await validation.ValidateAsync(request.CommentDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var comment = await _commentRepository.GetById(request.CommentDto.Id);
            comment.Content = request.CommentDto.Content;
            await _commentRepository.Update(comment);

            return Unit.Value;
        }
    }
}
