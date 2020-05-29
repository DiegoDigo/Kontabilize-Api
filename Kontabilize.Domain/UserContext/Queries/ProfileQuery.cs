using System;
using System.Linq.Expressions;
using Kontabilize.Domain.UserContext.Entities;

namespace Kontabilize.Domain.UserContext.Queries
{
    public static class ProfileQuery
    {
        public static Expression<Func<Profile, bool>> FindByUserId(Guid id)
        {
            return x => x.User.Id == id;
        }

        public static Expression<Func<Profile, bool>> FindById(Guid id)
        {
            return x => x.Id == id;
        }
    }
}