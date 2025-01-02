using Microsoft.AspNetCore.Mvc;
using School.Api.Base;
using School.Core.Features.User.Commands.Models;
using School.Core.Features.User.Queries.Models;
using School.Data.AppMetaData;

namespace School.Api.Controllers
{

    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost(Router.ApplicationUserRouting.Create)]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }
        [HttpGet(Router.ApplicationUserRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(Router.ApplicationUserRouting.GetByID)]
        public async Task<IActionResult> GetUserByID([FromRoute] string id)
        {
            return NewResult(await Mediator.Send(new GetUserByIdQuery(id)));
        }
        [HttpPut(Router.ApplicationUserRouting.Edit)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }
        [HttpPut(Router.ApplicationUserRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }
        [HttpPut(Router.ApplicationUserRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }
    }
}
