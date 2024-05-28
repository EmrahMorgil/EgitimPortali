using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Core.Entities;
using EgitimPortali.Core.Enums;
using EgitimPortali.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        private readonly EfContext _context;
        public UserRepository(EfContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }
        public async Task<DashboardInstructorDto> GetInstructorDashboard(Guid InstructorId)
        {
            var instructorCourses = await (from userCourse in _context.UserCourse
                                           join course in _context.Course on userCourse.CourseId equals course.Id
                                           join instructor in _context.User on course.InstructorId equals instructor.Id
                                           where userCourse.Status == CourseStatus.Approved && instructor.Id == InstructorId
                                           select course)
                                          .ToListAsync();

            var dashboardDto = new DashboardInstructorDto
            {
                SoldCourseCount = instructorCourses.Count(),
                TotalRevenue = (int)instructorCourses.Sum(c => c.Price)
            };

            return dashboardDto;
        }

        public async Task<DashboardStudentDto> GetStudentDashboard(Guid StudentId)
        {
            var studentCourses = await (from userCourse in _context.UserCourse
                                        join course in _context.Course on userCourse.CourseId equals course.Id
                                        where userCourse.Status == CourseStatus.Approved && userCourse.UserId == StudentId
                                        select course)
                                          .ToListAsync();

            var dashboardDto = new DashboardStudentDto
            {
                PurchasedCourseCount = studentCourses.Count(),
                TotalSpentMoney = (int)studentCourses.Sum(c => c.Price)
            };

            return dashboardDto;
        }
    }
}
