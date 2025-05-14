using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.BlogCategoryMapping.Validators
{
    public class CreateBlogCategoryMappingDtoValidator : AbstractValidator<CreateBlogCategoryMappingDto>
    {
        private readonly IBlogCategoryMappingRepository _blogCategoryMappingRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateBlogCategoryMappingDtoValidator(IBlogCategoryMappingRepository blogCategoryMappingRepository, IBlogRepository blogRepository, ICategoryRepository categoryRepository)
        {
            _blogCategoryMappingRepository = blogCategoryMappingRepository;
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;

            RuleFor(p => p)
                .MustAsync(async (blogCategoryMappingDto, token) =>
                {
                    var blogCategoryMappingExists = await _blogCategoryMappingRepository.Exists(blogCategoryMappingDto.BlogId, blogCategoryMappingDto.CategoryId);
                    return !blogCategoryMappingExists;
                }).WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.BlogId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(async (id, token) => 
                {
                    var blogExists = await _blogRepository.Exists(id);
                    return blogExists;
                }).WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(async (id, token) =>
                {
                    var categoryExists = await _categoryRepository.Exists(id);
                    return categoryExists;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
