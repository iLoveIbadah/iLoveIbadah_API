using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.Blog.Validators;
using iLoveIbadah.Application.Features.Blogs.Requests.Commands;
using iLoveIbadah.Application.Responses;
using iLoveIbadah.Domain;
using MediatR;

namespace iLoveIbadah.Application.Features.Blogs.Handlers.Commands
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, BaseCommandResponse>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlobFileRepository _blobFileRepository;
        private readonly IMapper _mapper;

        public CreateBlogCommandHandler(IBlogRepository blogRepository, IBlobFileRepository blobFileRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _blobFileRepository = blobFileRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBlogCommand request, CancellationToken cannCancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBlogDtoValidator(_blobFileRepository);
            var validationResult = await validator.ValidateAsync(request.BlogDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                //throw new ValidationException(validationResult);
                //throw new ValidationException(validationResult.ToString()); // is it correct? to verify so: TODO
                return response; // is this correcto or do i need to throw an exception? TODO cause if i trow exception the response won't be returned to the client ant it contains the list or errors
            }

            var blog = _mapper.Map<Blog>(request.BlogDto);
            blog = await _blogRepository.Create(blog);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = blog.Id;

            return response;
        }
    }
}
