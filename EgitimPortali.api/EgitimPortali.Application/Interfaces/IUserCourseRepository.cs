using EgitimPortali.Application.Dtos;
using EgitimPortali.Core.Entities;
using EgitimPortali.Core.Enums;

namespace EgitimPortali.Application.Interfaces
{
    public interface IUserCourseRepository : IGenericRepository<UserCourse>
    {
        Task<CourseDto> GetUserCourseDetails(Guid id);
        Task<List<StudentManagementDto>> GetStudentManagementList(Guid InstructorId);
        List<UserCourse> FilteredUserCourseList(Guid UserId, CourseStatus CourseStatus);
    }
}
