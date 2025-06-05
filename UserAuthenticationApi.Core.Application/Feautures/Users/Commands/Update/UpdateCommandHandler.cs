using AutoMapper;
using MediatR;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Application.Interfaces.IServices;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Unit>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IJwtGeneratorService _jwtGeneratorService;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(
            IUsersRepository userRepository,
            IJwtGeneratorService jwtGeneratorService,
            IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtGeneratorService = jwtGeneratorService;
        }

        public Task<Unit> Handle(UpdateCommand commnad, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
