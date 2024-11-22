using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Response;

namespace School.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }
        public int studentpageSize { get; set; }
        public int studentpageNumber { get; set; }



    }
}
