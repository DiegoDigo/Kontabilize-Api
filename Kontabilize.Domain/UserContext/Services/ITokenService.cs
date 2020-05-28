using Kontabilize.Domain.UserContext.Entities;

namespace Kontabilize.Domain.UserContext.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}