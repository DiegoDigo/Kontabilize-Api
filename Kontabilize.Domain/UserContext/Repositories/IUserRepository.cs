using System.Threading.Tasks;
using Kontabilize.Domain.UserContext.Entities;

namespace Kontabilize.Domain.UserContext.Repositories
{
    public interface IUserRepository
    {
        Task Save(User user);
        Task<User> FindByEmail(string email);
        Task<bool> ExistEmail(string email);
    }
}