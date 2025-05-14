using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.Category.Validators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryDtoValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0)
                .MustAsync(async (id, token) => 
                {
                    var categoryExists = await _categoryRepository.Exists(id);
                    return categoryExists;
                }).WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.FullName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 255 characters.")
                .MustAsync(async (fullName, token) =>
                {
                    var categoryExists = await _categoryRepository.Exists(fullName);
                    return !categoryExists; // ensure the category name is unique in db
                }).WithMessage("{PropertyName} already exists.");
        }
    }
}
