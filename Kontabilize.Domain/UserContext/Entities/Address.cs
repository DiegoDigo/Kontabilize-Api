using Kontabilize.Shared;
using Kontabilize.Shared.Entities;

namespace Kontabilize.Domain.UserContext.Entities
{
    public class Address : Entity
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public Address()
        {
        }

        public Address(string street, string city, string country, string zipCode)
        {
            Street = street;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }
        
        public Address EditAddress(string street, string city, string country, string zipCode)
        {
            Street = Utilitaty.Equals(this.Street, street);
            City = Utilitaty.Equals(this.City, city);
            Country = Utilitaty.Equals(this.Country, country);
            ZipCode = Utilitaty.Equals(this.ZipCode, zipCode);
            return this;
        }
    }
}