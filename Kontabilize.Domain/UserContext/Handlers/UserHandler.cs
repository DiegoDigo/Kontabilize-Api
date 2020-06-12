using System.Threading.Tasks;
using FluentValidator;
using Kontabilize.Domain.UserContext.Command.Input;
using Kontabilize.Domain.UserContext.Command.Output;
using Kontabilize.Domain.UserContext.Entities;
using Kontabilize.Domain.UserContext.Entities.Enums;
using Kontabilize.Domain.UserContext.Repositories;
using Kontabilize.Domain.UserContext.Services;
using Kontabilize.Shared.Command;
using Kontabilize.Shared.Handlers;
using Kontabilize.Shared.VOs;

namespace Kontabilize.Domain.UserContext.Handlers
{
    public class UserHandler : Notifiable,
        IHandler<SignInCommand>,
        IHandler<SignUpCommand>,
        IHandler<ResetPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<CommandResult> Handler(SignInCommand command)
        {
            if (!command.Validated())
            {
                return new CommandResult(false, "Error accessing user.", command.Notifications);
            }

            var user = await _userRepository.FindByEmail(command.Email);

            if (user == null)
            {
                AddNotification("user", "User not exist.");
                return new CommandResult(false, "Error accessing user.", Notifications);
            }

            if (user.VerifyPassword(command.Password))
            {
                return new CommandResult(true, "Login successfully",
                    new SignInCommandResponse(_tokenService.GenerateToken(user)));
            }

            AddNotification("user", "User or password not exist.");
            return new CommandResult(false, "Error accessing user.", Notifications);
        }

        public async Task<CommandResult> Handler(SignUpCommand command)
        {
            if (!command.Validated())
            {
                return new CommandResult(false, "Error created user.", command.Notifications);
            }

            if (await _userRepository.ExistEmail(command.Email))
            {
                AddNotification("Email", "Email already registered.");
                return new CommandResult(false, "Error created user.", Notifications);
            }

            var email = new Email(command.Email);
            var user = new User(email, ERole.CUSTOMER);
            user.HasPassword(command.Password);

            await _userRepository.Save(user);

            return new CommandResult(true, "Login successfully", user);
        }

        public async Task<CommandResult> Handler(ResetPasswordCommand command)
        {
            if (command.Invalid)
            {
                return new CommandResult(false, "Error to find user", command.Notifications);
            }

            var user = await _userRepository.FindByEmail(command.Email);
            if (user == null)
            {
                AddNotification("User", "user not found.");
                return new CommandResult(false, "Error to find user", command.Notifications);
            }


            return new CommandResult(true, "Generety token.", new ResetPasswordCommandResponse(user.Id.ToString(), ""));
        }
    }
}