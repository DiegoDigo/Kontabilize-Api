using System;
using System.Linq.Expressions;
using Kontabilize.Domain.UserContext.Entities;

namespace Kontabilize.Domain.UserContext.Queries
{
    public static class UserQuery
    {
        public static Expression<Func<User, bool>> FindByEmail(string email)
        {
            return x => x.Email.Address.Equals(email);
        }

        public static Expression<Func<User, bool>> FindById(Guid id)
        {
            return x => x.Id == id;
        }
    }
}