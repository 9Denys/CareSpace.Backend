using CareSpace.Backend.Application.Services.ServiceCategory.Commands;
using FluentValidation;

namespace CareSpace.Backend.Application.Common.Validator.ServiceCategory
{
    public class CreateServiceCategoryCommandValidator
        : AbstractValidator<CreateServiceCategoryCommand>
    {
        public CreateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.ServiceCategory.Name)
                .NotEmpty().WithMessage("Service category name is required")
                .MinimumLength(2).WithMessage("Service category name must be at least 2 symbols")
                .MaximumLength(100).WithMessage("Service category name must not be longer than 100 symbols");

            RuleFor(x => x.ServiceCategory.Description)
                .MaximumLength(500).WithMessage("Description must not be longer than 500 symbols");
        }
    }
}