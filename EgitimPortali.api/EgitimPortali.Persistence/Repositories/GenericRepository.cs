using EgitimPortali.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> Create(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> List()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
