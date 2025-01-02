using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;

namespace School.Core.Features.Authentication.Queries.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public ConfirmEmailValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            _ = RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("UserId Must not be empty")
                 .NotNull().WithMessage("UserId Must not be Null");

            _ = RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code Must not be Empty")
                .NotNull().WithMessage("Code Must not be Null");
        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion
    }
}
