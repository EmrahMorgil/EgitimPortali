using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using EgitimPortali.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EgitimPortali.Application.Handlers.UserCourse.Queries
{
    public class ListUserCourseQuery : IRequest<BaseDataResponse<List<UserCourseDto>>>
    {
        public class ListUserCourseQueryHandler : IRequestHandler<ListUserCourseQuery, BaseDataResponse<List<UserCourseDto>>>
        {
            IUserCourseRepository _userCourseRepository;
            ICourseRepository _courseRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ListUserCourseQueryHandler(IUserCourseRepository userCourseRepository, ICourseRepository courseRepository,IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _userCourseRepository = userCourseRepository;
                _courseRepository = courseRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseDataResponse<List<UserCourseDto>>> Handle(ListUserCourseQuery request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                var userCourseDtoList = new List<UserCourseDto>();

                var userCourseList = await _userCourseRepository.List();
                var filteredUserCourseList = userCourseList.Where(x => x.UserId == tokenInfo.UserId && x.Status != CourseStatus.Discarded);

                foreach (var course in filteredUserCourseList)
                {
                    var userCourseDto = new UserCourseDto();
                    userCourseDto.Id = course.Id;
                    userCourseDto.Course = _mapper.Map<CourseDto>(await _courseRepository.GetById(course.CourseId));
                    userCourseDto.Status = course.Status;
                    userCourseDtoList.Add(userCourseDto);
                }

                return new BaseDataResponse<List<UserCourseDto>>(userCourseDtoList, true, ResponseMessages.Success);
            }
        }
    }
}
