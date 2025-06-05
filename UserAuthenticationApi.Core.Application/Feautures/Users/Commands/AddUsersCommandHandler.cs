using AutoMapper;
using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Helpers;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Application.Interfaces.IServices;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands
{
    public class AddUsersCommandHandler : IRequestHandler<AddUsersCommand, Unit>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IJwtGeneratorService _jwtGeneratorService;
        private readonly IMapper _mapper;
        
        public AddUsersCommandHandler(
            IUsersRepository userRepository, 
            IJwtGeneratorService jwtGeneratorService,
            IMapper mapper) 
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtGeneratorService = jwtGeneratorService;
        }

        public async Task<Unit> Handle(AddUsersCommand command, CancellationToken cancellationToken)
        {
            //Revisar si el email ya existe
            var response = await _userRepository.EmailExistanceAsync(command.Email);
            if (response) throw new ApiExeption("El email ya está en uso");
            
            //Encriptar la contraseña
            PasswordEncryptator.HashUserPassword384(command.Password);

            //Mapeo de entidades
           var adduser = _mapper.Map<UserAuthenticationApi.Core.Domain.Entities.Users>(command);

            //Generar el JWT para el usuario
           adduser.Token = await _jwtGeneratorService.GenerateJwt(adduser);

            //Persistir el usuario
            await _userRepository.AddAsync(adduser);

            return Unit.Value;
        }
    }
}
