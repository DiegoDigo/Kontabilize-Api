using FluentValidator;
using FluentValidator.Validation;
using Kontabilize.Shared.Command;

namespace Kontabilize.Domain.UserContext.Command.Input
{
    public class SignInCommand : Notifiable, ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool Validated()
        {
            AddNotifications(
                new ValidationContract()
                    .Requires()
                    .IsEmail(Email, "Email", "Email is invalid.")
                    .HasMaxLen(Email, 160, "Email", "Email must have a maximum of 160 characters.")
                    .HasMinLen(Password, 8, "Senha", "Password must be at least 8 characters long.")
                    .HasMaxLen(Password, 16, "Senha", "Password must be a maximum of 16 characters.")
            );
            return Valid;
        }
    }
}