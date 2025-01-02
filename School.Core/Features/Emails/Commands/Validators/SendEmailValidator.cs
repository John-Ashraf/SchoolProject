using FluentValidation;
using School.Core.Features.Emails.Commands.Models;

namespace School.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Constructors
        public SendEmailValidator()
        {
            ApplyValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            _ = RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email must not be empty")
                 .NotNull().WithMessage("Email must not be null");

            _ = RuleFor(x => x.Message)
                 .NotEmpty().WithMessage("Message must not be empty")
                 .NotNull().WithMessage("Message must not be null");
        }
        #endregion
    }
}
