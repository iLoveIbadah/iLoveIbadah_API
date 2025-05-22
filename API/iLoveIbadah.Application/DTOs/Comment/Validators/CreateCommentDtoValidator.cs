using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Comment.Validators
{
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IBlogRepository _blogRepository;

        public CreateCommentDtoValidator(ICommentRepository commentRepository, IUserAccountRepository userAccountRepository, IBlogRepository blogRepository)
        {
            _commentRepository = commentRepository;
            _userAccountRepository = userAccountRepository;
            _blogRepository = blogRepository;

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.BlogId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(async (id, token) =>
                {
                    var blogExists = await _blogRepository.Exists(id);
                    return blogExists;
                }).WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.UserAccountId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(async (id, token) =>
                {
                    var userAccountExists = await _userAccountRepository.Exists(id.Value);
                    return userAccountExists;
                }).WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.ParentCommentId)
                .MustAsync(async (id, token) =>
                {
                    if (id == null)
                        return true; // if it is top level comment! then it has no parent comment
                    var commentExists = await _commentRepository.Exists((int)id);
                    return commentExists;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
