using CareSpace.Backend.Application.Services.ServiceCentre.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.ServiceCentre
{
    public class CreateServiceCentreCommandValidator
        : AbstractValidator<CreateServiceCentreCommand>
    {
        public CreateServiceCentreCommandValidator()
        {
            RuleFor(x => x.ServiceCentre.Address)
                .NotEmpty().WithMessage("Service centre address is required")
                .MinimumLength(5).WithMessage("Address must be at least 5 symbols")
                .MaximumLength(300).WithMessage("Address must not be longer than 300 symbols");

            RuleFor(x => x.ServiceCentre.OpenTime)
                .NotEmpty().WithMessage("Open time is required");

            RuleFor(x => x.ServiceCentre.CloseTime)
                .NotEmpty().WithMessage("Close time is required");

            RuleFor(x => x.ServiceCentre)
                .Must(x => x.OpenTime < x.CloseTime)
                .WithMessage("Open time must be earlier than close time");
        }
    }
}