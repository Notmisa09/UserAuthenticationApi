namespace UserAuthenticationApi.Core.Application.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(dynamic? Id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity, dynamic Id);
        Task RemoveAsync(T entity);
    }
}
