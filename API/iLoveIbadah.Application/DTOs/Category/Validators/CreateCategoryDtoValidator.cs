using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Category.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryDtoValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(p => p.FullName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.")
                .MustAsync(async (fullName, token) =>
                {
                    var categoryExists = await _categoryRepository.Exists(fullName);
                    return !categoryExists; // ensure the category name is unique in db
                }).WithMessage("{PropertyName} already exists.");

        }
    }
}
