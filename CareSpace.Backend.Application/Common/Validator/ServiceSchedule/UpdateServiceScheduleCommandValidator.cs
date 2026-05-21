using CareSpace.Backend.Application.Services.ServiceSchedule.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.ServiceSchedule
{
    public class UpdateServiceScheduleCommandValidator
        : AbstractValidator<UpdateServiceScheduleCommand>
    {
        public UpdateServiceScheduleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Service schedule id is required");

            RuleFor(x => x.ServiceSchedule.ServiceId)
                .NotEmpty().WithMessage("Service id is required");

            RuleFor(x => x.ServiceSchedule.SlotId)
                .NotEmpty().WithMessage("Time slot id is required");

            RuleFor(x => x.ServiceSchedule.CentreId)
                .NotEmpty().WithMessage("Service centre id is required");
        }
    }
}