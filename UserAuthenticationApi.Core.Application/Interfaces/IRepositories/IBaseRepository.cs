namespace UserAuthenticationApi.Core.Application.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int? Id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity, int Id);
        Task RemoveAsync(T entity);
    }
}
