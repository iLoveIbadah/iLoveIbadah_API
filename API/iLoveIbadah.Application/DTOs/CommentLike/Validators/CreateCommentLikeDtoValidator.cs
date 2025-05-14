using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.CommentLike.Validators
{
    public class CreateCommentLikeDtoValidator : AbstractValidator<CreateCommentLikeDto>
    {
        private readonly ICommentLikeRepository _commentLikeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserAccountRepository _userAccountRepository;

        public CreateCommentLikeDtoValidator(ICommentLikeRepository commentLikeRepository, ICommentRepository commentRepository, IUserAccountRepository userAccountRepository)
        {
            _commentLikeRepository = commentLikeRepository;
            _commentRepository = commentRepository;
            _userAccountRepository = userAccountRepository;

            RuleFor(p => p)
                .MustAsync(async (commentLikeDto, token) =>
                {
                    var commentLikeExists = await _commentLikeRepository.Exists(commentLikeDto.CommentId, commentLikeDto.UserAccountId);
                    return !commentLikeExists;
                })
                .WithMessage("CommentLike Already Exists");

            RuleFor(p => p.CommentId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .MustAsync(async (commentId, token) =>
                {
                    var commentExists = await _commentRepository.Exists(commentId);
                    return commentExists;
                })
                .WithMessage("Comment does not exist");

            RuleFor(p => p.UserAccountId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .MustAsync(async (userAccountId, token) =>
                {
                    var userAccountExists = await _userAccountRepository.Exists(userAccountId);
                    return userAccountExists;
                })
                .WithMessage("UserAccount does not exist");
        }
    }
}
