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
        public async Task<T> AddAsync(T entity)
        {
            await set.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
             set.Remove(entity);
             await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAllAsync() => await set.ToListAsync();

        public async Task<T?> GetByIdAsync(int? Id) => await _dbContext.Set<T>().FindAsync(Id);

        public async Task UpdateAsync(T entity, int Id)
        {
            var entry = await set.FindAsync(Id);
            if (entry == null) _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
