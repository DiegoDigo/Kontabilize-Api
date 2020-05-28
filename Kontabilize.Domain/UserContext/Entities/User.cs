using System;
using Kontabilize.Domain.UserContext.Entities.Enums;
using Kontabilize.Shared.Entities;
using Kontabilize.Shared.Utility;
using Kontabilize.Shared.VOs;

namespace Kontabilize.Domain.UserContext.Entities
{
    public class User : Entity
    {
        public Email Email { get; private set; }
        public string Password { get; private set; }
        public ERole Role { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreateAt { get; private set; }

        public User()
        {
        }

        public User(Email email, ERole role)
        {
            Email = email;
            Role = role;
            Active = false;
            CreateAt = DateTime.Now.Date;
        }

        public void Activate()
        {
            Active = true;
        }

        public void HasPassword(string password)
        {
            Password = PasswordUtil.Hash(password);
        }

        public bool VerifyPassword(string inputPassword)
        {
            return PasswordUtil.Verify(inputPassword, Password);
        }
        
    }
}