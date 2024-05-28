using EgitimPortali.Application.Dtos;
using EgitimPortali.Core.Entities;

namespace EgitimPortali.Application.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<DashboardInstructorDto> GetInstructorDashboard(Guid InstructorId);
        Task<DashboardStudentDto> GetStudentDashboard(Guid StudentId);
    }
}
