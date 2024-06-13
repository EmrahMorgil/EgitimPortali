using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Handlers.Course.Commands;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Services;
using EgitimPortali.Core.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EgitimPortali.Application.Handlers.Course.Queries
{
    public class ListCourseQuery : IRequest<List<CourseDto>>
    {
        public class ListCourseQueryHandler : IRequestHandler<ListCourseQuery, List<CourseDto>>
        {
            ICourseRepository _courseRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ListCourseQueryHandler(ICourseRepository courseRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _courseRepository = courseRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<List<CourseDto>> Handle(ListCourseQuery request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                if(tokenInfo.Role == Role.Instructor)
                {
                    var courseList = await _courseRepository.GetCourseListByInstructorId(tokenInfo.UserId);
                    return courseList;
                }
                else
                {
                    var courseList = (await _courseRepository.List()).Select(x => _mapper.Map<CourseDto>(x)).ToList();
                    return courseList;
                }
            }
        }
    }
}
