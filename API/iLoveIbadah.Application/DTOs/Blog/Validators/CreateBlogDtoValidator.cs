using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Blog.Validators
{
    public class CreateBlogDtoValidator : AbstractValidator<CreateBlogDto>
    {
        private readonly IBlobFileRepository _blobFileRepository;

        public CreateBlogDtoValidator(IBlobFileRepository blobFileRepository)
        {
            _blobFileRepository = blobFileRepository;

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
