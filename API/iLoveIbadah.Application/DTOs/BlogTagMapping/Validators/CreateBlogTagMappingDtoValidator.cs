using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.BlogTagMapping.Validators
{
    public class CreateBlogTagMappingDtoValidator : AbstractValidator<CreateBlogTagMappingDto>
    {
        private readonly IBlogTagMappingRepository _blogTagMappingRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IBlogRepository _blogRepository;

        public CreateBlogTagMappingDtoValidator(IBlogTagMappingRepository blogTagMappingRepository, ITagRepository tagRepository, IBlogRepository blogRepository)
        {
            _blogTagMappingRepository = blogTagMappingRepository;
            _tagRepository = tagRepository;
            _blogRepository = blogRepository;

            RuleFor(p => p)
                .MustAsync(async (blogTagMappingDto, cancellationToken) =>
                {
                    var blogTagMappingExists = await _blogTagMappingRepository.Exists(blogTagMappingDto.TagId, blogTagMappingDto.BlogId);
                    return !blogTagMappingExists; // ensure the blog tag mapping is unique in db
                }).WithMessage("Blog tag mapping already exists");

            RuleFor(p => p.TagId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var tagExists = await _tagRepository.Exists(id);
                    return tagExists;
                }).WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.BlogId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var blogExists = await _blogRepository.Exists(id);
                    return blogExists;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
