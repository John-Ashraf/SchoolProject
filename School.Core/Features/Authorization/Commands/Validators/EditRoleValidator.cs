using FluentValidation;
using School.Core.Features.Authorization.Commands.Models;

namespace School.Core.Features.Authorization.Commands.Validators
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields

        #endregion
        #region Constructors
        public EditRoleValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            _ = RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Id must not be empty")
                 .NotNull().WithMessage("Id must not be Null");

            _ = RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Name must not be Empty")
                 .NotNull().WithMessage("Name must not be Null");
        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}
