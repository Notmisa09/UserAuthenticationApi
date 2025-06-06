using MediatR;
using System.ComponentModel.DataAnnotations;
using UserAuthenticationApi.Core.Application.Dtos;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Login
{
    public class LoginCommand : IRequest<UsersDto>
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
