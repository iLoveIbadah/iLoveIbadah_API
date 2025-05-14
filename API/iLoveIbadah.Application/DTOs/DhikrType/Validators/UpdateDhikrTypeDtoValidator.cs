using FluentValidation;
using iLoveIbadah.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.DhikrType.Validators
{
    public class UpdateDhikrTypeDtoValidator : AbstractValidator<UpdateDhikrTypeDto>
    {
        private readonly IDhikrTypeRepository _dhikrTypeRepository;

        public UpdateDhikrTypeDtoValidator(IDhikrTypeRepository dhikrTypeRepository)
        {
            _dhikrTypeRepository = dhikrTypeRepository;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var dhikrTypeExists = await _dhikrTypeRepository.Exists(id);
                    return dhikrTypeExists;
                })
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.FullName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");

            //TODO add validation for unique fullname createdby!
        }
    }
}
