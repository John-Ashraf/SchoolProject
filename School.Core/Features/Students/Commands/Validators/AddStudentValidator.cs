using FluentValidation;
using School.Core.Features.Students.Commands.Models;
using School.Services.Abstracts;

namespace School.Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion
        #region Constructors
        public AddStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name).
                NotEmpty().WithMessage("Name Must ")
                .NotNull().WithMessage("Name Must Be Not Null")
                .MaximumLength(200).WithMessage("Max Length is 10");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyName} Must Be Null")
                .MaximumLength(100).WithMessage("{PropertyName} Length is 100");


        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsExist(Key)).WithMessage("Name is Exist");
        }
        #endregion

    }
}
