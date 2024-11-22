using MediatR;
using School.Core.Features.Students.Response;
using School.Core.Wrappers;
using School.Data.Helpers;

namespace School.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedQuery : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum orderby { get; set; }
        public string? Search { get; set; }
    }
}
