using FluentValidator;
using FluentValidator.Validation;
using Kontabilize.Shared.Command;

namespace Kontabilize.Domain.CompanyContext.Commands.Inputs
{
    public class CreateMigrateCompanyCommand : Notifiable, ICommand
    {
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FixPhone { get; set; }
        public string MobilePhone { get; set; }
        public string CompanyTracking { get; set; }
        
        
        public bool Validated()
        {
            AddNotifications(
                new ValidationContract()
                    .Requires()
                    .IsEmail(Email,"Email","Invalid email.")
                    .IsNullOrEmpty(FirstName, "First Name", "First name is required.")
                    .IsNotNullOrEmpty(LastName, "Last Name", "Last name is required.")
                    .IsNullOrEmpty(Cnpj, "Cnpj", "Cnpj is required.")
                    .HasLen(Cnpj, 14,"Cnpj", "Cnpj must be 11 characters")
                    .HasLen(FixPhone, 10, "Fix Phone", "Fix Phone must be 8 characters plus the DDD.")
                    .HasLen(MobilePhone, 11, "Fix Phone", "Fix Phone must be 9 characters plus the DDD.")
                    .IsNullOrEmpty(CompanyTracking, "Company Tracking", "Company Tracking is required."));
            return Valid;
        }
    }
}