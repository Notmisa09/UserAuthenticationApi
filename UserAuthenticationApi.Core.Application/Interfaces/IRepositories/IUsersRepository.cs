using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Core.Application.Interfaces.IRepositories
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        Task<bool> EmailExistanceAsync(string email);
        Task<IList<Users>> GetUserWithPhones();
        Task<bool> EmailIsUnique(string Email);
    }
}
