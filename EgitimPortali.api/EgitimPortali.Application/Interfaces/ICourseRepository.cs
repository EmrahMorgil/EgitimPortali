using EgitimPortali.Application.Dtos;
using EgitimPortali.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Application.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<CourseDto> GetCourseDetails(Guid courseId, Guid userId);

        Task<List<CourseDto>> GetCourseListByInstructorId(Guid instructorId);
    }
}
