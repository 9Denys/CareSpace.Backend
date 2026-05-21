using CareSpace.Backend.Application.Services.Service.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.Service
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Service id is required");

            RuleFor(x => x.Service.CategoryId)
                .NotEmpty().WithMessage("Service category id is required");

            RuleFor(x => x.Service.Title)
                .NotEmpty().WithMessage("Service title is required")
                .MinimumLength(2).WithMessage("Service title must be at least 2 symbols")
                .MaximumLength(150).WithMessage("Service title must not be longer than 150 symbols");

            RuleFor(x => x.Service.Description)
                .MaximumLength(1000).WithMessage("Description must not be longer than 1000 symbols");

            RuleFor(x => x.Service.DurationMinutes)
                .GreaterThan(0).WithMessage("Duration must be greater than 0")
                .LessThanOrEqualTo(480).WithMessage("Duration must not be longer than 480 minutes");
        }
    }
}