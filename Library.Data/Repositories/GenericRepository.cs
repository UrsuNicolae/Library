using Library.Data.Data;
using Library.Data.Models;
using Library.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Library.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        internal readonly DataContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            try
            {
                var existingEntity = await GetById(entity.Id);
                if (existingEntity != null)
                {
                    throw new ArgumentException($"Entity: {nameof(T)} already exists!");
                }
            }
            catch (KeyNotFoundException)
            {
                //
            }
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existingEntity = await GetById(id);
            _dbSet.Remove(existingEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity: {typeof(T).Name} not found!");
            }
            return entity;
        }

        public async Task Update(T entity)
        {
            var existingEntity = await GetById(entity.Id);
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
