using CareSpace.Backend.Application.Services.Appointment.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.Appointment
{
    public class CreateAppointmentCommandValidator
        : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(x => x.Appointment.ScheduleId)
                .NotEmpty().WithMessage("Schedule id is required");
        }
    }
}