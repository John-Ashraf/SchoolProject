using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Models;
using School.Core.Features.Students.Response;
using School.Core.Wrappers;
using School.Data.Entities;
using School.Services.Abstracts;
using System.Linq.Expressions;

namespace School.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandlers : ResponseHandler,
        IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
        IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudentResponse>>,
        IRequestHandler<GetStudentPaginatedQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentQueryHandlers(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentsList = await _studentService.GetAllStudents();
            var resultStudent = _mapper.Map<List<GetStudentListResponse>>(studentsList);
            var result = Success(resultStudent);
            result.Meta = new { Count = studentsList.Count() };
            return result;
        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id); // Note: await added for async call
            if (student == null)
            {
                return NotFound<GetSingleStudentResponse>();
            }

            var result = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(result);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e =>
            new GetStudentPaginatedListResponse(e.StudID, e.Name, e.Address, e.Phone, e.Department.DName.ToString());
            var FilterQuery = _studentService.FilterQuerable(request.orderby, request.Search);
            var result = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            result.Meta = new { Count = result.Data.Count };
            return result;


        }
    }
}
