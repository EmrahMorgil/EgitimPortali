using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EgitimPortali.Application.Handlers.User.Queries
{
    public class DashboardInstructorQuery : IRequest<BaseDataResponse<DashboardInstructorDto>>
    {
        public class DashboardInstructorQueryHandler : IRequestHandler<DashboardInstructorQuery, BaseDataResponse<DashboardInstructorDto>>
        {
            IUserRepository _userRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public DashboardInstructorQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
            {
                _userRepository = userRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseDataResponse<DashboardInstructorDto>> Handle(DashboardInstructorQuery request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                var dashboardInstructor = await _userRepository.GetInstructorDashboard(tokenInfo.UserId);
                return new BaseDataResponse<DashboardInstructorDto>(dashboardInstructor, true, ResponseMessages.Success);
            }
        }
    }
}
