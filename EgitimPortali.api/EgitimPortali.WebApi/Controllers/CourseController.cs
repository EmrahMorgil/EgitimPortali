using EgitimPortali.Application.Handlers.Course.Commands;
using EgitimPortali.Application.Handlers.Course.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create")]
        [Authorize(Roles = Application.Consts.Role.Instructor)]
        public async Task<ActionResult> Create(CreateCourseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("delete")]
        [Authorize(Roles = Application.Consts.Role.Instructor)]
        public async Task<ActionResult> Delete(DeleteCourseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("update")]
        [Authorize(Roles = Application.Consts.Role.Instructor)]
        public async Task<ActionResult> Update(UpdateCourseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpGet("detail")]
        [Authorize]
        public async Task<ActionResult> Detail([FromQuery]DetailCourseQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("list")]
        [Authorize]
        public async Task<ActionResult> List([FromQuery]ListCourseQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
