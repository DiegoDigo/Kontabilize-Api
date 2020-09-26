using System.Collections.Generic;
using System.Linq;
using Kontabilize.Domain.UserContext.Entities;
using Kontabilize.Domain.UserContext.Entities.Enums;
using Kontabilize.Shared.VOs;
using Microsoft.EntityFrameworkCore;

namespace Kontabilize.Infra.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            Users().ToList().ForEach(user =>
            {
                modelBuilder.Entity<User>(userEntity =>
                {
                    userEntity.HasData(MapUserToAnonymousObject(user));
                    userEntity.OwnsOne(x => x.Email).HasData(MapEmailToAnonymousObject(user));
                });
            });
        }

        private static object MapUserToAnonymousObject(User user)
        {
            return new
            {
                user.Id,
                user.Active,
                user.Password,
                user.Role,
                user.CreateAt
            };
        }
        
        private static object MapEmailToAnonymousObject(User user)
        {
            return new
            {
                UserId = user.Id,
                user.Email.Address,
            };
        }

        private static IEnumerable<User> Users() 
        {
            return new List<User>
            {
                InitialUserAdmin(),
                InitialUserAccountant(),
                InitialUserCustomer()
            };
        }
        
        private static User InitialUserAdmin()
        {
            var email = new Email("admin@gmail.com");
            var user = new User(email, ERole.Admin);
            user.HasPassword("12345678");
            return user;
        }
        
        private static User InitialUserAccountant()
        {
            var email = new Email("contador@gmail.com");
            var user = new User(email, ERole.Accountant);
            user.HasPassword("12345678");
            return user;
        }
        
        private static User InitialUserCustomer()
        {
            var email = new Email("usuario@gmail.com");
            var user = new User(email, ERole.Customer);
            user.HasPassword("12345678");
            return user;
        }
    }
}