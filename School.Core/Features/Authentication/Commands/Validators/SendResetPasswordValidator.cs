using FluentValidation;
using School.Core.Features.Authentication.Commands.Models;

namespace School.Core.Features.Authentication.Commands.Validators
{
    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {
        public SendResetPasswordValidator()
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


        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion
    }
}
