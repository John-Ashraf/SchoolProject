using FluentValidation;
using School.Core.Features.Authentication.Commands.Models;

namespace School.Core.Features.Authentication.Commands.Validators
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #region Actions
        public void ApplyValidationsRules()
        {
            _ = RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email must be not empty")
                 .NotNull().WithMessage("Email must be not null");
            _ = RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("Password must be not empty")
                 .NotNull().WithMessage("Password must be not null");
            _ = RuleFor(x => x.ConfirmPassword)
                 .NotEmpty().WithMessage("ConfirmPassword must be not empty")
                 .NotNull().WithMessage("ConfirmPassword must be not null");
            _ = RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("ConfirmPassword must match Password");


        }


        public void ApplyCustomValidationsRules()
        {

        }
        #endregion
    }
}

