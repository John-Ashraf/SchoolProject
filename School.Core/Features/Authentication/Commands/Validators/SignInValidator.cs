using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;

namespace School.Core.Features.Authentication.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public SignInValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            _ = RuleFor(x => x.UserName)
                 .NotEmpty().WithMessage("UserName must not be empty")
                 .NotNull().WithMessage("UserName must not be NULL");

            _ = RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must not be empty")
                .NotNull().WithMessage("Password must not be Null");
        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion
    }
}
