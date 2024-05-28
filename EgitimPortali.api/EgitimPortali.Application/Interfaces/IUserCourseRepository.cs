using EgitimPortali.Application.Dtos;
using EgitimPortali.Core.Entities;

namespace EgitimPortali.Application.Interfaces
{
    public interface IUserCourseRepository : IGenericRepository<UserCourse>
    {
        Task<CourseDto> GetUserCourseDetails(Guid id);
        Task<List<StudentManagementDto>> GetStudentManagementList(Guid InstructorId);
    }
}
