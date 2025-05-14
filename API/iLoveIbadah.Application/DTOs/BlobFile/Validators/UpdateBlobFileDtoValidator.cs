using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.BlobFile.Validators
{
    public class UpdateBlobFileDtoValidator : AbstractValidator<UpdateBlobFileDto>
    {
        public UpdateBlobFileDtoValidator()
        {
            RuleFor(p => p.FullName)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
