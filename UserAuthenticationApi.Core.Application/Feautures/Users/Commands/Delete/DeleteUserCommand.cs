using AutoMapper;
using MediatR;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
