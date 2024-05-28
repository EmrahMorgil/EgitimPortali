using AutoMapper;
using EgitimPortali.Application.Handlers.Course.Commands;
using EgitimPortali.Application.Handlers.Course.Queries;
using EgitimPortali.Application.Handlers.User.Commands;
using EgitimPortali.Application.Handlers.User.Queries;
using EgitimPortali.Application.Handlers.UserCourse.Commands;
using EgitimPortali.Application.Handlers.UserCourse.Queries;
using EgitimPortali.Application.Mapping;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EgitimPortali.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Mapper register
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GeneralMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Mediatr register
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                //User
                typeof(CreateUserCommand).Assembly,
                typeof(UpdateUserCommand).Assembly,
                typeof(DashboardStudentQuery).Assembly,
                typeof(DetailUserQuery).Assembly,
                typeof(LoginUserQuery).Assembly,
                //Course
                typeof(CreateCourseCommand).Assembly,
                typeof(DeleteCourseCommand).Assembly,
                typeof(UpdateCourseCommand).Assembly,
                typeof(DetailCourseQuery).Assembly,
                typeof(ListCourseQuery).Assembly,
                //UserCourse
                typeof(CreateUserCourseCommand).Assembly,
                typeof(DeleteUserCourseCommand).Assembly,
                typeof(ListUserCourseQuery).Assembly,
                typeof(ListStudentManagementQuery).Assembly
            ));
        }
    }
}
