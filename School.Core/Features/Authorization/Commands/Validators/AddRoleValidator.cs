using FluentValidation;

using School.Core.Features.Authorization.Commands.Models;
using School.Services.Abstracts;

namespace School.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRolesCommand>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region Constructors

        #endregion
        public AddRoleValidator(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            _ = RuleFor(x => x.RoleName)
                 .NotEmpty().WithMessage("RoleName must not be empty")
                 .NotNull().WithMessage("RoleName must not be Null");
        }

        public void ApplyCustomValidationsRules()
        {
            _ = RuleFor(x => x.RoleName)
                .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleExistByName(Key))
                .WithMessage("The Role Exist Before");
        }

        #endregion
    }

}
