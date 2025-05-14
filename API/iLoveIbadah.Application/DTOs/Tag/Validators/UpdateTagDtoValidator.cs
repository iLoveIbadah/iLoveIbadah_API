using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Tag.Validators
{
    public class UpdateTagDtoValidator : AbstractValidator<UpdateTagDto>
    {
        private readonly ITagRepository _tagRepository;

        public UpdateTagDtoValidator(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{Id} is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("{Id} must be greater than 0.")
                .MustAsync(async (id, token) => 
                {
                    var tagExists = await _tagRepository.Exists(id);
                    return tagExists;
                }).WithMessage("{Id} does not exist.");

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
