using AutoMapper;
using MediatR;
using UserAuthenticationApi.Core.Application.Dtos;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UsersDto>
    {
            
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public LoginCommandHandler(
            IUsersRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public Task<UsersDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
