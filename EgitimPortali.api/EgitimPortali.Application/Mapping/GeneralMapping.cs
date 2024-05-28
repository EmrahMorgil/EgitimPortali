using AutoMapper;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Handlers.Course.Commands;
using EgitimPortali.Application.Handlers.User.Commands;
using EgitimPortali.Application.Handlers.UserCourse.Commands;
using EgitimPortali.Core.Entities;

namespace EgitimPortali.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //User
            CreateMap<User, UserDto>();
            CreateMap<CreateUserCommand, User>();

            //Course
            CreateMap<Course, CourseDto>();
            CreateMap<CreateCourseCommand, Course>();

            //UserCourse
            CreateMap<UserCourse, UserCourseDto>();
            CreateMap<CreateUserCourseCommand, UserCourse>();

            //Documentation
            CreateMap<Documentation, DocumentationDto>();
        }
    }
}
