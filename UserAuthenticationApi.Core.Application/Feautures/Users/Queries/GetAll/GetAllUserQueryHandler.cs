using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Dtos;
using UserAuthenticationApi.Core.Application.Feautures.Users.Queries.GetAll;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Queries
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQuery, IList<UserResGetAllDto>>
    {
        private readonly IUsersRepository _userRepository;
        public GetAllUserQueryHandler(IUsersRepository userRepository) => _userRepository = userRepository;
        
        public async Task<IList<UserResGetAllDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUserWithPhones();

            if (users.Count() == 0) throw new ApiException("No hay usuarios regitrados aun", 400);

            var userWithPhone = users.Select(x => new UserResGetAllDto
            {
                Email = x.Email,               
                Name = x.Name,
                Password = x.Password,
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
