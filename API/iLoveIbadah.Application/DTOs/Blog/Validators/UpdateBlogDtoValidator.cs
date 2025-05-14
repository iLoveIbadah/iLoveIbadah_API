using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Blog.Validators
{
    public class UpdateBlogDtoValidator : AbstractValidator<UpdateBlogDto>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlobFileRepository _blobFileRepository;

        public UpdateBlogDtoValidator(IBlogRepository blogRepository, IBlobFileRepository blobFileRepository)
        {
            _blogRepository = blogRepository;
            _blobFileRepository = blobFileRepository;

            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} must be present.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.")
                .MustAsync(async (id, token) => 
                {
                    var blogExists = await _blogRepository.Exists(id);
                    return blogExists;
                });

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Slug)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.BlobFileId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(async (id, cancellationToken) =>
                {
                    var blobFileExists = await _blobFileRepository.Exists(id);
                    return blobFileExists;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
