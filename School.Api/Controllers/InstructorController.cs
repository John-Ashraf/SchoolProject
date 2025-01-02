using Microsoft.AspNetCore.Mvc;
using School.Api.Base;
using School.Core.Features.Instructors.Commands.Models;
using School.Data.AppMetaData;

namespace School.Api.Controllers
{
    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpPost(Router.InstructorRouting.AddInstructor)]
        public async Task<IActionResult> AddInstructor([FromForm] AddInstructorCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}
