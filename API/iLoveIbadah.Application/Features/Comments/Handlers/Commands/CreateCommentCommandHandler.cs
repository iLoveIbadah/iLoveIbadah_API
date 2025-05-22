using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.Comment.Validators;
using iLoveIbadah.Application.Features.Comments.Requests.Commands;
using iLoveIbadah.Application.Responses;
using iLoveIbadah.Domain;
using MediatR;

namespace iLoveIbadah.Application.Features.Comments.Handlers.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, BaseCommandResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public CreateCommentCommandHandler(ICommentRepository commentRepository, IUserAccountRepository userAccountRepository, IBlogRepository blogRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userAccountRepository = userAccountRepository;
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCommentDtoValidator(_commentRepository, _userAccountRepository, _blogRepository);
            var validationResult = await validator.ValidateAsync(request.CommentDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                //throw new ValidationException(validationResult);
                return response;
            }

            var comment = _mapper.Map<Comment>(request.CommentDto);
            comment = await _commentRepository.Create(comment);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = comment.Id;

            return response;
        }
    }
}
