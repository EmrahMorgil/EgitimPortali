using EgitimPortali.Application.Handlers.User.Commands;
using EgitimPortali.Application.Handlers.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgitimPortali.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<ActionResult> Create(CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginUserQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult> Update(UpdateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("detail")]
        [Authorize]
        public async Task<ActionResult> Detail(DetailUserQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("student/dashboard")]
        [Authorize(Roles = Application.Consts.Role.Student)]
        public async Task<ActionResult> StudentDashboard([FromQuery]DashboardStudentQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("instructor/dashboard")]
        [Authorize(Roles = Application.Consts.Role.Instructor)]
        public async Task<ActionResult> InstructorDashboard([FromQuery]DashboardInstructorQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
