using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Tag.Validators
{
    public class CreateTagDtoValidator : AbstractValidator<CreateTagDto>
    {
        private readonly ITagRepository _tagRepository;

        public CreateTagDtoValidator(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

            RuleFor(p => p.FullName)
                .NotEmpty().WithMessage("{FullName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{FullName} must not exceed 50 characters.")
                .MustAsync(async (fullName, token) =>
                {
                    var tagExists = await _tagRepository.Exists(fullName);
                    return !tagExists; // ensure the tag name is unique in db
                }).WithMessage("{PropertyName} already exists.");
        }
    }
}
