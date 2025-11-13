using Microsoft.EntityFrameworkCore;
using MyStore.Application.Interfaces;
using MyStore.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            // Set the specific DbSet (e.g., Products, Categories)
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            // FindAsync is optimized for finding by primary key
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // Get all items from this specific DbSet
            return await _dbSet.ToListAsync();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            // Return the DbSet as IQueryable to allow further filtering
            return _dbSet.AsQueryable();
        }

        public void Add(TEntity entity)
        {
            // This just marks the entity as 'Added' in the change tracker.
            // It does NOT hit the database.
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            // This just marks the entity as 'Modified' in the change tracker.
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            // This just marks the entity as 'Deleted' in the change tracker.
            _dbSet.Remove(entity);
        }
    }
}