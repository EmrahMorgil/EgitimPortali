using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EgitimPortali.Application.Handlers.User.Queries
{
    public class DashboardStudentQuery : IRequest<BaseDataResponse<DashboardStudentDto>>
    {
        public class DashboardStudentQueryHandler : IRequestHandler<DashboardStudentQuery, BaseDataResponse<DashboardStudentDto>>
        {
            IUserRepository _userRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public DashboardStudentQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
            {
                _userRepository = userRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseDataResponse<DashboardStudentDto>> Handle(DashboardStudentQuery request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                var dashboardStudent = await _userRepository.GetStudentDashboard(tokenInfo.UserId);
                return new BaseDataResponse<DashboardStudentDto>(dashboardStudent, true, ResponseMessages.Success);
            }
        }
    }
}
