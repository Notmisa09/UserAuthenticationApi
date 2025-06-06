using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Dtos;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Create
{
    public class AddUsersCommand : IRequest<Result<UserResAddDto>>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public IList<PhoneReqAddDto> Phones { get; set; } = [];
    }
}
