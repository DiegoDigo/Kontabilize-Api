using System.Linq;
using System.Threading.Tasks;
using Kontabilize.Domain.UserContext.Entities;
using Kontabilize.Domain.UserContext.Queries;
using Kontabilize.Domain.UserContext.Repositories;
using Kontabilize.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Kontabilize.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KontabilizeContext _context;

        public UserRepository(KontabilizeContext context)
        {
            _context = context;
        }

        public async Task Save(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindByEmail(string email) => await _context.Users.Where(UserQuery.FindByEmail(email))
            .AsNoTracking().SingleOrDefaultAsync();

        public async Task<bool> ExistEmail(string email) => await _context.Users.AnyAsync(UserQuery.FindByEmail(email));
    }
}