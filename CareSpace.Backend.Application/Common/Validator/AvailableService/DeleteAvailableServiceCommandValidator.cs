using CareSpace.Backend.Application.Services.AvailableService.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.AvailableService
{
    public class DeleteAvailableServiceCommandValidator
        : AbstractValidator<DeleteAvailableServiceCommand>
    {
        public DeleteAvailableServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Available service id is required");
        }
    }
}