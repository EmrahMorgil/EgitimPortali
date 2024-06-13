using EgitimPortali.Application.Handlers.UserCourse.Commands;
using EgitimPortali.Application.Handlers.UserCourse.Queries;
using EgitimPortali.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserCourseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create")]
        [Authorize(Roles = nameof(Role.Student))]
        public async Task<ActionResult> Create(CreateUserCourseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("delete")]
        [Authorize(Roles = nameof(Role.Student))]
        public async Task<ActionResult> Delete(DeleteUserCourseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpGet("list")]
        [Authorize(Roles = nameof(Role.Student))]
        public async Task<ActionResult> List([FromQuery]ListUserCourseQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult> Update(UpdateUserCourseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpGet("detail")]
        [Authorize]
        public async Task<ActionResult> Update([FromQuery]DetailUserCourseQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("studentManagementList")]
        [Authorize(Roles = nameof(Role.Instructor))]
        public async Task<ActionResult> List([FromQuery]ListStudentManagementQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
