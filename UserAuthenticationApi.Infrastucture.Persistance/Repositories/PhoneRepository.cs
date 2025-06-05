using Microsoft.EntityFrameworkCore;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Infrastucture.Persistance.Repositories
{
    public class PhoneRepository : BaseRepository<Phone>, IPhoneRepository
    {
        private readonly DbContext _context;
        public PhoneRepository(DbContext context) : base(context) => _context = context;
    }
}
