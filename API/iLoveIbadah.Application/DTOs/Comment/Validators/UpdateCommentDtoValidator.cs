using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Comment.Validators
{
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserAccountRepository _userAccountRepository;

        public UpdateCommentDtoValidator(ICommentRepository commentRepository, IUserAccountRepository userAccountRepository)
        {
            _commentRepository = commentRepository;
            _userAccountRepository = userAccountRepository;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(async (id, token) =>
                {
                    var commentExists = await _commentRepository.Exists(id);
                    return commentExists;
                }).WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.UserAccountId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(async (id, token) =>
                {
                    var userAccountExists = await _userAccountRepository.Exists(id.Value);
                    return userAccountExists;
                }).WithMessage("{PropertyName} does not exist.");

            // Règle pour vérifier que l'utilisateur est l'auteur du commentaire
            RuleFor(p => p)
                .MustAsync(async (commentDto, token) =>
                {
                    if (commentDto.Id <= 0 || commentDto.UserAccountId == null)
                        return false;

                    var comment = await _commentRepository.GetById(commentDto.Id);
                    return comment.UserAccountId == commentDto.UserAccountId;
                }).WithMessage("You can only update your own comments.");
        }
    }
}
