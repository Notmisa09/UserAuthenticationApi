using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Core.Application.Interfaces.IServices
{
    public interface IJwtGeneratorService
    {
        Task<string> GenerateJwt(Users user);
    }
}