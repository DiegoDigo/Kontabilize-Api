using System;

namespace Kontabilize.Domain.UserContext.Command.Output
{
    public class ProfileCommandResponse
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FixPhone { get; set; }
        public string MobilePhone { get; set; }
        public string ImageUrl { get; set; }
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public ProfileCommandResponse(Guid id, string cpf, string cnpj, string email, string firstName,
            string lastName, string fixPhone, string mobilePhone, string imageUrl, Guid userId, Guid addressId,
            string street, string city, string country, string zipCode)
        {
            Id = id;
            Cpf = cpf;
            Cnpj = cnpj;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            FixPhone = fixPhone;
            MobilePhone = mobilePhone;
            ImageUrl = imageUrl;
            UserId = userId;
            AddressId = addressId;
            Street = street;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }
    }
}