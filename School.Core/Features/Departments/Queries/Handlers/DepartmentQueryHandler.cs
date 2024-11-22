using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Models;
using School.Core.Features.Departments.Queries.Response;
using School.Core.Wrappers;
using School.Data.Entities;
using School.Services.Abstracts;
using System.Linq.Expressions;

namespace School.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
                                                        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private IMapper _mapper;
        #endregion

        #region Constructors
        public DepartmentQueryHandler(IDepartmentService departmentService, IMapper mapper, IStudentService studentService)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _studentService = studentService;
        }

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {

            var respone = await _departmentService.GetDepartmentById(request.Id);
            if (respone == null) { return NotFound<GetDepartmentByIdResponse>("No Department Exists With This Id"); }

            var result = _mapper.Map<GetDepartmentByIdResponse>(respone);
            // pagination
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Name);
            var studentquerable = _studentService.GetQuearableStudents().Where(st => st.DID == request.Id).AsNoTracking();
            var studentlist = await studentquerable.Select(expression).ToPaginatedListAsync(request.studentpageNumber, request.studentpageSize);
            result.StudentList = studentlist;

            return Success(result);


        }

        #endregion

        #region HandleFunctions

        #endregion


    }
}
