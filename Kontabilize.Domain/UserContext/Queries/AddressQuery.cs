using System;
using System.Linq.Expressions;
using Kontabilize.Domain.UserContext.Entities;

namespace Kontabilize.Domain.UserContext.Queries
{
    public static class AddressQuery
    {
        public static Expression<Func<Address, bool>> FindById(Guid id)
        {
            return x => x.Id == id;
        }
    }
}