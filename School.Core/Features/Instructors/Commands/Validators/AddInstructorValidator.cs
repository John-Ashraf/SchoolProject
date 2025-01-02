using FluentValidation;
using School.Core.Features.Instructors.Commands.Models;
using School.Services.Abstracts;

namespace School.Core.Features.Instructors.Commands.Validators
{
    public class AddInstructorValidator : AbstractValidator<AddInstructorCommand>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IInstructorService _instructorService;
        #endregion
        #region Constructors
        public AddInstructorValidator(IDepartmentService departmentService, IInstructorService instructorService)
        {
            _departmentService = departmentService;
            _instructorService = instructorService;
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            _ = RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Name Must not Be Empty")
                 .NotNull().WithMessage("Name Must Not Null");


            _ = RuleFor(x => x.DID)
                .NotEmpty().WithMessage("Department Must be not empty")
                .NotNull().WithMessage("Department Must be not Null");
        }

        public void ApplyCustomValidationsRules()
        {
            _ = RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _instructorService.IsNameExist(Key))
                .WithMessage("Name Exist Before");

            _ = RuleFor(x => x.DID)
           .MustAsync(async (Key, CancellationToken) => await _departmentService.IsExist(Key))
           .WithMessage("Department Is not Exist");

        }

        #endregion
    }
}
