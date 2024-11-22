using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Students.Commands.Models;
using School.Data.Entities;
using School.Services.Abstracts;
using SchoolProject.Core.Resources;
namespace School.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
                                                       IRequestHandler<AddStudentCommand, Response<string>>,
                                                       IRequestHandler<EditStudentCommand, Response<string>>,
                                                       IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields 
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region Constructors 
        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        #endregion
        #region Handlers
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);

            var result = await _studentService.AddAsync(studentMapper);

            if (result == "Success")
            {
                return Created<string>("Added Successfully");
            }
            else
            {
                return BadRequest<string>();
            }

        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return NotFound<string>();
            }
            var studentmapper = _mapper.Map<Student>(request);
            var result = await _studentService.EditAsync(studentmapper);
            if (result == "Success")
            {
                return Success<string>("");
            }
            return UnprocessableEntity<string>(result);

        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return NotFound<string>("Student Not Found");
            }
            var result = await _studentService.DeleteAsync(student);
            if (result == "Success") return Deleted<string>($"Deleted Successfully {request.Id}");
            return BadRequest<string>();
        }
        #endregion
    }
}
