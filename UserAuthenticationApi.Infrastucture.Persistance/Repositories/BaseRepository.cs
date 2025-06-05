using Microsoft.EntityFrameworkCore;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;

namespace UserAuthenticationApi.Infrastucture.Persistance.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        protected DbSet<T> set => _dbContext.Set<T>();
        public BaseRepository(DbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await set.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task RemoveAsync(T entity)
        {
             set.Remove(entity);
             await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IList<T>> GetAllAsync() => await set.ToListAsync();

        public virtual async Task<T?> GetByIdAsync(dynamic? Id) => await _dbContext.Set<T>().FindAsync(Id);

        public virtual async Task UpdateAsync(T entity, dynamic Id)
        {
            var entry = await set.FindAsync(Id);
            if (entry == null) _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
