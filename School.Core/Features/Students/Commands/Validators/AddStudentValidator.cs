using FluentValidation;
using School.Core.Features.Students.Commands.Models;
using School.Services.Abstracts;

namespace School.Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        #endregion
        #region Constructors
        public AddStudentValidator(IStudentService studentService, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region Handle Functions
        public void ApplyValidationsRules()
        {
            _ = RuleFor(x => x.Name).
                NotEmpty().WithMessage("Name Must ")
                .NotNull().WithMessage("Name Must Be Not Null")
                .MaximumLength(200).WithMessage("Max Length is 10");

            _ = RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyName} Must Be Null")
                .MaximumLength(100).WithMessage("{PropertyName} Length is 100");

            _ = RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
            .NotNull().WithMessage("{PropertyName} Must Be Null");


        }
        public void ApplyCustomValidationsRules()// el false hwa el bedrb error
        {
            _ = RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsExist(Key)).WithMessage("Name is Exist");
            _ = RuleFor(x => x.DepartmentId)
                .MustAsync(async (Key, CancellationToken) => await _departmentService.IsExist(Key)).WithMessage("Department is not Exist");
        }
        #endregion

    }
}
