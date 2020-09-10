using System;
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

        public async Task<User> GetUserById(Guid id) =>
            await _context.Users.SingleOrDefaultAsync(UserQuery.FindById(id));

        public async Task<User> FindByEmail(string email) =>
            await _context.Users.SingleOrDefaultAsync(UserQuery.FindByEmail(email));

        public async Task<bool> ExistEmail(string email) =>
            await _context.Users.AnyAsync(UserQuery.FindByEmail(email));
    }
}