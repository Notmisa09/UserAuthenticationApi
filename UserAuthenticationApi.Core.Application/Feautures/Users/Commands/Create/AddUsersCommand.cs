﻿using MediatR;
using UserAuthenticationApi.Core.Application.Dtos;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Create
{
    public class AddUsersCommand : IRequest<Unit>
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
