using CareSpace.Backend.Application.Services.Appointment.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.Appointment
{
    public class CancelAppointmentCommandValidator
        : AbstractValidator<CancelAppointmentCommand>
    {
        public CancelAppointmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Appointment id is required");
        }
    }
}