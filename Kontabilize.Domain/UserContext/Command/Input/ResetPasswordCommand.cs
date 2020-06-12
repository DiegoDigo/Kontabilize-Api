using FluentValidator;
using FluentValidator.Validation;
using Kontabilize.Shared.Command;

namespace Kontabilize.Domain.UserContext.Command.Input
{
    public class ResetPasswordCommand: Notifiable, ICommand
    {

        public string Email { get; set; }
        
        
        public bool Validated()
        {
            AddNotifications(
                new ValidationContract()
                    .Requires()
                    .IsEmail(Email, "Email", "Email is required.")
                );
            return Valid;
        }
    }
}