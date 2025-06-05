using AutoMapper;
using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Dtos;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Queries.GetAll
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQuery, IList<UsersDto>>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUserQueryHandler(
            IUsersRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IList<UsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUserWithPhones();
            users.Where(x => x.IsActive == true);
            if (users.Count() == 0) throw new ApiExeption("No hay usuarios regitrados aun");
            var userslist = _mapper.Map<IList<UsersDto>>(users);
            return userslist;
        }
    }
}
