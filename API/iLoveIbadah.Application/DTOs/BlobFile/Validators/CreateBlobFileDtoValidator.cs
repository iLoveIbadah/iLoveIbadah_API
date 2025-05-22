using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using iLoveIbadah.Application.DTOs.BlobFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.BlobFile.Validators
{
    public class CreateBlobFileDtoValidator : AbstractValidator<CreateBlobFileDto>
    {
        private readonly IBlobFileRepository _blobFileRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        public CreateBlobFileDtoValidator(IBlobFileRepository blobFileRepository, IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
            _blobFileRepository = blobFileRepository;

            RuleFor(p => p.FullName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(35).WithMessage("{PropertyName} must not exceed 35 characters.");

            RuleFor(p => p.Uri)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(300).WithMessage("{PropertyName} must not exceed 300 characters.")
                .MustAsync(async (uri, token) =>
                {
                    var blobFileExists = await _blobFileRepository.Exists(uri);
                    return !blobFileExists; // ensure the blob file Uri is unique in db
                });

            RuleFor(p => p.Extension)
                .NotNull();

            RuleFor(p => p.CreatedBy)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var userAccountExists = await _userAccountRepository.Exists(id);
                    return userAccountExists;
                })
                .WithMessage("{PropertyName} does not exist.");

            //RuleFor(p => p.Size)
        }
    }
}
