using CareSpace.Backend.Application.Services.ServiceSchedule.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.ServiceSchedule
{
    public class CreateServiceScheduleCommandValidator
        : AbstractValidator<CreateServiceScheduleCommand>
    {
        public CreateServiceScheduleCommandValidator()
        {
            RuleFor(x => x.ServiceSchedule.ServiceId)
                .NotEmpty().WithMessage("Service id is required");

            RuleFor(x => x.ServiceSchedule.SlotId)
                .NotEmpty().WithMessage("Time slot id is required");

            RuleFor(x => x.ServiceSchedule.CentreId)
                .NotEmpty().WithMessage("Service centre id is required");
        }
    }
}