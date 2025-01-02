using FluentValidation;
using School.Core.Features.User.Commands.Models;

namespace School.Core.Features.User.Commands.Validators
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region fields
        #endregion
        #region constructor
        public ChangeUserPasswordValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region handlefunctions
        public void ApplyValidationsRules()
        {
            _ = RuleFor(x => x.id).NotEmpty().WithMessage("Id must not be empty")
                .NotNull().WithMessage("Id must not be empty");


            _ = RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyName} Must Be Null")
                .MaximumLength(100).WithMessage("{PropertyName} Length is 100");

            _ = RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
            .NotNull().WithMessage("{PropertyName} Must Be Null");


            _ = RuleFor(x => x.ConfirmPassword)
           .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
           .NotNull().WithMessage("{PropertyName} Must Be Null")
           .Equal(c => c.NewPassword).WithMessage("The Confirm Password dosent match the password");


        }
        public void ApplyCustomValidationsRules()// el false hwa el bedrb error
        {

        }
    }
    #endregion
}
