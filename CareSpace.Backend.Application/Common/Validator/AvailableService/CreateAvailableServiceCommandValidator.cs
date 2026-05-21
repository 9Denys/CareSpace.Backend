using CareSpace.Backend.Application.Services.AvailableService.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.AvailableService
{
    public class CreateAvailableServiceCommandValidator
        : AbstractValidator<CreateAvailableServiceCommand>
    {
        public CreateAvailableServiceCommandValidator()
        {
            RuleFor(x => x.AvailableService.UserId)
                .NotEmpty().WithMessage("User id is required");

            RuleFor(x => x.AvailableService.ServiceId)
                .NotEmpty().WithMessage("Service id is required");
        }
    }
}