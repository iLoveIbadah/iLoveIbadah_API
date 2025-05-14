using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.BlogLike.Validators
{
    public class CreateBlogLikeDtoValidator : AbstractValidator<CreateBlogLikeDto>
    {
        private readonly IBlogLikeRepository _blogLikeRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserAccountRepository _userAccountRepository;

        public CreateBlogLikeDtoValidator(IBlogLikeRepository blogLikeRepository, IBlogRepository blogRepository, IUserAccountRepository userAccountRepository)
        {
            _blogLikeRepository = blogLikeRepository;
            _blogRepository = blogRepository;
            _userAccountRepository = userAccountRepository;

            RuleFor(p => p)
                .MustAsync(async (dto, token) =>
                {
                    var blogLikeExists = await _blogLikeRepository.Exists(dto.BlogId, dto.UserAccountId);
                    return !blogLikeExists;
                })
                .WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.BlogId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(async (id, token) =>
                {
                    var blogExists = await _blogRepository.Exists(id);
                    return blogExists;
                }).WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.UserAccountId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(async (id, token) =>
                {
                    var userAccountExists = await _userAccountRepository.Exists(id);
                    return userAccountExists;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
