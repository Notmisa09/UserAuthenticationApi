using Microsoft.EntityFrameworkCore;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Infrastucture.Persistance.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        private readonly ApplicationContext _context;
        public UsersRepository(ApplicationContext context) : base(context) => _context = context;

        public async Task<bool> EmailExistanceAsync(string email) => await _context.Users.Where(x => x.Email == email).AnyAsync();
        public async Task<IList<Users>> GetUserWithPhones() => await _context.Users.Where(x => x.IsActive).Include(x => x.Phones).ToListAsync();
        public async Task<bool> VerifyPasswordAsync(string password) => await _context.Users.Where(x => x.Password == password).AnyAsync();
        public async Task<Users> GetUserByEmail(string email) => await _context.Users.Where(x => x.Email == email ).Include(x => x.Phones).FirstAsync();

        public async Task<bool> EmailIsUnique(string Email)
        {
            if(await EmailExistanceAsync(Email))
            {
                throw new Exception("Email already exists");
            };

            return false;
        }


    }
}
