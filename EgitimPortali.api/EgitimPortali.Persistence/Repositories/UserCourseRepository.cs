using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Core.Entities;
using EgitimPortali.Core.Enums;
using EgitimPortali.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Persistence.Repositories
{
    public class UserCourseRepository : GenericRepository<UserCourse>, IUserCourseRepository
    {
        private readonly DbSet<UserCourse> _dbSet;
        private readonly EfContext _context;
        public UserCourseRepository(EfContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<UserCourse>();
        }

        public async Task<List<StudentManagementDto>> GetStudentManagementList(Guid InstructorId)
        {
            var userCourseDetails = await (from userCourse in _context.UserCourse
                                           join course in _context.Course on userCourse.CourseId equals course.Id
                                           join user in _context.User on userCourse.UserId equals user.Id
                                           where course.InstructorId == InstructorId
                                           select new StudentManagementDto
                                           {
                                               Id = userCourse.Id,
                                               User = new UserDto() { Id = user.Id, Name = user.Name, Image = user.Image, Role = user.Role, Email = user.Email },
                                               Course = new CourseDto() { Id = course.Id, Title = course.Title, Description = course.Description },
                                               CourseStatus = userCourse.Status
                                           }).ToListAsync();
            return userCourseDetails;
        }

        public async Task<CourseDto> GetUserCourseDetails(Guid id)
        {
            var userCourseDetails = await (from userCourse in _context.UserCourse
                                           join course in _context.Course on userCourse.CourseId equals course.Id
                                           join instructor in _context.User on course.InstructorId equals instructor.Id
                                           where userCourse.Id == id && userCourse.Status == CourseStatus.Approved
                                           select new CourseDto
                                           {
                                               Id = course.Id,
                                               Documentations = new List<DocumentationDto>(),
                                               Instructor = new UserDto { Id = instructor.Id, Name = instructor.Name, Image = instructor.Image, Role = instructor.Role, Email = instructor.Email },
                                               EducationType = course.EducationType,
                                               Title = course.Title,
                                               Description = course.Description,
                                               Capacity = course.Capacity,
                                               Price = course.Price,
                                               Time = course.Time,
                                               IntroductionPhoto = course.IntroductionPhoto,
                                               IntroductionVideo = course.IntroductionVideo,
                                           }).FirstOrDefaultAsync();
            return userCourseDetails;
        }
    }
}

