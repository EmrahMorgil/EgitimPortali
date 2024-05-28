using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Core.Entities;
using EgitimPortali.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgitimPortali.Persistence.Repositories
{
    public class DocumentationRepository : GenericRepository<Documentation>, IDocumentationRepository
    {
        private readonly DbSet<Documentation> _dbSet;
        private readonly EfContext _context;

        public DocumentationRepository(EfContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Documentation>();
        }

        public async Task<List<Documentation>> GetDocumentationsByCourseId(Guid id)
        {
            return await _dbSet.Where(item => item.CourseId == id).ToListAsync();
        }

        public async Task<bool> DeleteDocumentationsByCourseId(Guid id)
        {
            var documentations = await _dbSet.Where(item => item.CourseId == id).ToListAsync();

            if (documentations.Any())
            {
                _dbSet.RemoveRange(documentations);
                await _context.SaveChangesAsync();
                return true; 
            }

            return false;
        }
    }
}
