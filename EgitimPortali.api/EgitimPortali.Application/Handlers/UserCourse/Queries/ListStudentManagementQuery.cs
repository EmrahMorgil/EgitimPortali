using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EgitimPortali.Application.Handlers.UserCourse.Queries
{
    public class ListStudentManagementQuery : IRequest<BaseDataResponse<List<StudentManagementDto>>>
    {
        public class ListStudentManagementQueryHandler : IRequestHandler<ListStudentManagementQuery, BaseDataResponse<List<StudentManagementDto>>>
        {
            ICourseRepository _courseRepository;
            IUserRepository _userRepository;
            IUserCourseRepository _userCourseRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ListStudentManagementQueryHandler(ICourseRepository courseRepository, IUserRepository userRepository, IUserCourseRepository userCourseRepository, IMapper mapper,
                IHttpContextAccessor httpContextAccessor)
            {
                _courseRepository = courseRepository;
                _userRepository = userRepository;
                _userCourseRepository = userCourseRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseDataResponse<List<StudentManagementDto>>> Handle(ListStudentManagementQuery request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                var studentManagementList = await _userCourseRepository.GetStudentManagementList(tokenInfo.UserId);
                return new BaseDataResponse<List<StudentManagementDto>>(studentManagementList, true, ResponseMessages.Success);
            }
        }
    }
}
