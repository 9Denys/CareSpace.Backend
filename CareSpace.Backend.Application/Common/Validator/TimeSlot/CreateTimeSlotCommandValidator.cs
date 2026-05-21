using CareSpace.Backend.Application.Services.TimeSlot.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.TimeSlot
{
    public class CreateTimeSlotCommandValidator
        : AbstractValidator<CreateTimeSlotCommand>
    {
        public CreateTimeSlotCommandValidator()
        {
            RuleFor(x => x.TimeSlot.Date)
                .NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.TimeSlot.StartTime)
                .NotEmpty().WithMessage("Start time is required");

            RuleFor(x => x.TimeSlot.EndTime)
                .NotEmpty().WithMessage("End time is required");

            RuleFor(x => x.TimeSlot)
                .Must(x => x.StartTime < x.EndTime)
                .WithMessage("Start time must be earlier than end time");
        }
    }
}