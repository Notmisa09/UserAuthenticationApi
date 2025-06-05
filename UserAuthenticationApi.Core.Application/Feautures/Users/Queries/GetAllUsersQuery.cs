using MediatR;
using UserAuthenticationApi.Core.Application.Dtos;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IList<UsersDto>>
    {

    }
}
