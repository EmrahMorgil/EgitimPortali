using EgitimPortali.Application.Dtos;
using EgitimPortali.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Application.Interfaces
{
    public interface IDocumentationRepository : IGenericRepository<Documentation>
    {
        Task<List<Documentation>> GetDocumentationsByCourseId(Guid id);
        Task<bool> DeleteDocumentationsByCourseId(Guid id);
    }
}
