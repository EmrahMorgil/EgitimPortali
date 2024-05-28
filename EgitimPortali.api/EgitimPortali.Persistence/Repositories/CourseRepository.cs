using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Core.Entities;
using EgitimPortali.Core.Enums;
using EgitimPortali.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Persistence.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly DbSet<Course> _dbSet;
        private readonly EfContext _context;
        public CourseRepository(EfContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Course>();
        }

        public async Task<CourseDto> GetCourseDetails(Guid courseId, Guid userId)
        {
            var courseDetails = await (from course in _context.Course
                                       join instructor in _context.User on course.InstructorId equals instructor.Id
                                       join userCourse in _context.UserCourse.Where(uc => uc.UserId == userId) on course.Id equals userCourse.CourseId into uc
                                       from userCourse in uc.DefaultIfEmpty()
                                       where course.Id == courseId
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
                                           CourseStatus = userCourse != null ? userCourse.Status : CourseStatus.Subscribe
                                       }).FirstOrDefaultAsync();
            return courseDetails;
        }

        public async Task<List<CourseDto>> GetCourseListByInstructorId(Guid instructorId)
        {
            var courseList = await _context.Course
                                  .Where(course => course.InstructorId == instructorId)
                                  .Select(course => new CourseDto
                                  {
                                      Id = course.Id,
                                      Title = course.Title,
                                      Description = course.Description,
                                      Capacity = course.Capacity,
                                      Price = course.Price,
                                      Time = course.Time,
                                      IntroductionPhoto = course.IntroductionPhoto,
                                      IntroductionVideo = course.IntroductionVideo,
                                  })
                                  .ToListAsync();

            return courseList;
        }
    }
}
