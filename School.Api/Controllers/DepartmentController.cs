using Microsoft.AspNetCore.Mvc;
using School.Api.Base;
using School.Core.Features.Departments.Queries.Models;
using School.Data.AppMetaData;

namespace School.Api.Controllers
{

    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRounting.GetBYId)]
        public async Task<IActionResult> GetStudentById([FromQuery] GetDepartmentByIdQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
    }
}
