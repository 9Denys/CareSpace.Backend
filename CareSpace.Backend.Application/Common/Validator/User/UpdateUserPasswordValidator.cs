using CareSpace.Backend.Application.Services.User.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Application.Common.Validator.User
{
    public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordValidator()
        {
            RuleFor(x => x.UserPassword.CurrentPassword).NotEmpty();

            RuleFor(x => x.UserPassword.NewPassword)
                .MinimumLength(8).WithMessage("Password's length must be at least 8 symbols")
                .MaximumLength(32).WithMessage("Password's length mustn't be longer than 32 symbols")
                .NotEmpty();
        }
    }
}
