using Microsoft.EntityFrameworkCore;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Infrastucture.Persistance.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        private readonly ApplicationContext _context;
        public UsersRepository(ApplicationContext context) : base(context) => _context = context;
    }
}
