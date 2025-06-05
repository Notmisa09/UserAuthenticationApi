using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Dtos;
using UserAuthenticationApi.Core.Application.Feautures.Users.Queries.GetAll;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Queries
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQuery, IList<UsersDto>>
    {
        private readonly IUsersRepository _userRepository;
        public GetAllUserQueryHandler(IUsersRepository userRepository) => _userRepository = userRepository;
        
        public async Task<IList<UsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUserWithPhones();

            if (users.Count() == 0) throw new ApiExeption("No hay usuarios regitrados aun", 400);

            var userWithPhone = users.Where(x => x.IsActive).Select(x => new UsersDto 
            {
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                Email = x.Email,
                Id = x.Id,
                IsActive = x.IsActive,
                LastLogin = x.LastLogin,
                Name = x.Name,
                Password = x.Password,
                Token = x.Token,
                Phones = x.Phones.Select(p => new PhonesDto
                {
                    Id = p.Id,
                    UserId = p.Users.Id,
                    CityCode = p.CityCode,
                    CountryCode = p.CountryCode,
                    Number = p.Number,
                }).ToList(),
            }).ToList();

            return userWithPhone;
        }
    }
}
