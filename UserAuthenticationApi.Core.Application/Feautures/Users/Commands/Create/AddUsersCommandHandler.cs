using AutoMapper;
using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Dtos;
using UserAuthenticationApi.Core.Application.Helpers;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Application.Interfaces.IServices;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Create
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
            var email = await _userRepository.EmailExistanceAsync(command.Email);
            if (email) throw new ApiException("El email ya está en uso", 400);

            //Mapeo de entidades
            var adduser = _mapper.Map<Domain.Entities.Users>(command);

            //Establecer la fecha de login igual a la de creación
            adduser.LastLogin = DateTime.Now;

            //Encriptar la contraseña
            adduser.Password = PasswordEncryptator.HashUserPassword384(command.Password);

            //Generar el JWT para el usuario
            adduser.Token = await _jwtGeneratorService.GenerateJwt(adduser);

            //Persistir el usuario
            var user  = await _userRepository.AddAsync(adduser);
            if (user == null) throw new ApiException("No se pudo crear el usuario", 500);

            var response = _mapper.Map<UserResAddDto>(user);

            return Unit.Value;
        }
    }
}
