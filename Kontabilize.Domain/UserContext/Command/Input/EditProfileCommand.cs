using System;
using FluentValidator;
using FluentValidator.Validation;
using Kontabilize.Shared.Command;
using Microsoft.AspNetCore.Http;

namespace Kontabilize.Domain.UserContext.Command.Input
{
    public class EditProfileCommand : Notifiable, ICommand
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string FixPhone { get; set; }
        public string MobilePhone { get; set; }
        public IFormFile Image { get; set; }
        public string UserId { get; set; }
        public string AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        
        public bool Validated()
        {
            AddNotifications(
                new ValidationContract()
                    .Requires()
                    .IsNullOrEmpty(Id, "Id", "Profile id is required.")
                    .IsNullOrEmpty(FirstName, "First Name", "First name is required.")
                    .IsNotNullOrEmpty(LastName, "Last Name", "Last name is required.")
                    .IsNullOrEmpty(Cpf, "Cpf", "Cpf is required.")
                    .HasLen(Cpf, 11,"Cpf", "Cpf must be 11 characters")
                    .IsNullOrEmpty(Cnpj, "Cnpj", "Cnpj is required.")
                    .HasLen(Cnpj, 14,"Cpf", "Cnpj must be 14 characters")
                    .HasLen(FixPhone, 10, "Fix Phone", "Fix Phone must be 8 characters plus the DDD.")
                    .HasLen(MobilePhone, 11, "Fix Phone", "Fix Phone must be 9 characters plus the DDD.")
                    .IsNullOrEmpty(UserId, "User Id" , "User Id is required.")
                    .IsEmail(Email, "Email", "Email is invalid.").HasMaxLen(Email, 160, "Email", "Email must have a maximum of 160 characters.")
                    .IsNullOrEmpty(AddressId, "Address Id", "Address id is required.")
                    .IsNullOrEmpty(Street, "Street", "Street is required.")
                    .IsNullOrEmpty(City, "City", "City is required.")
                    .IsNullOrEmpty(Country, "Country", "Country is required.")
                    .IsNullOrEmpty(ZipCode, "Zip code", "Zip code id is required.")
                    .HasLen(ZipCode, 8, "Zip code", "Zip code must be 8 characters.")
            );
            
            return Valid;
        }
    }
}