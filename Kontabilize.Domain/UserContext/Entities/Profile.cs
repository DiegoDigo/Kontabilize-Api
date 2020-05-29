using System;
using Kontabilize.Shared;
using Kontabilize.Shared.Entities;
using Kontabilize.Shared.VOs;

namespace Kontabilize.Domain.UserContext.Entities
{
    public class Profile : Entity
    {
        public Name Name { get; private set; }
        public string ImageUrl { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public Guid UserId { get; set; }
        public User User { get; private set; }
        public Guid AddressId { get; set; }
        public Address Address { get; private set; }

        public Profile()
        {

        }

        public Profile(Name name, string imageUrl, Document document, Email email, Phone phone, User user, Address address)
        {
            Name = name;
            ImageUrl = imageUrl;
            Document = document;
            Email = email;
            Phone = phone;
            User = user;
            Address = address;
        }
        
        public Profile EditProfile(Name name, string imageUrl, Document document, Email email, Phone phone, User user, Address address)
        {
            this.Name = Utilitaty.Equals(this.Name, name);
            this.ImageUrl = Utilitaty.Equals(this.ImageUrl, imageUrl);
            this.Document = Utilitaty.Equals(this.Document, document);
            this.Email = Utilitaty.Equals(this.Email, email);
            this.Phone = Utilitaty.Equals(this.Phone, phone);
            this.User = Utilitaty.Equals(this.User, user);
            this.Address = Utilitaty.Equals(this.Address, address);
            return this;
        }

    }
}