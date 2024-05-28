using EgitimPortali.Application.Interfaces;
using EgitimPortali.Core.Entities;
using EgitimPortali.Persistence.Context;
using EgitimPortali.Persistence.Repositories;
using EgitimPortali.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EgitimPortali.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EfContext>(options =>
            options.UseSqlServer(Configuration.GetSettings<string>("ConnectionStrings:DefaultConnection")));

            //services.AddIdentityCore<User>(opt =>
            //{
            //    opt.Password.RequireNonAlphanumeric = false;
            //    opt.Password.RequiredLength = 2;
            //    opt.Password.RequireLowercase = false;
            //    opt.Password.RequireUppercase = false;
            //    opt.Password.RequireDigit = false;
            //    opt.SignIn.RequireConfirmedEmail = false;

            //}).AddRoles<Role>().AddEntityFrameworkStores<EfContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IUserCourseRepository, UserCourseRepository>();
            services.AddScoped<IDocumentationRepository, DocumentationRepository>();
        }
    }
}
