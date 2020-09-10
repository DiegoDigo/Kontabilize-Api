using System.Threading.Tasks;
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
    public class UserHandler :
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
                return new CommandResult(false, "User not exist.", null);
            }

            if (user.VerifyPassword(command.Password))
            {
                return new CommandResult(true, "Login successfully",
                    new TokenResponseCommand(_tokenService.GenerateToken(user)));
            }

            return new CommandResult(false, "User or password not exist.", null);
        }

        public async Task<CommandResult> Handler(SignUpCommand command)
        {
            if (!command.Validated())
            {
                return new CommandResult(false, "Error created user.", command.Notifications);
            }

            if (await _userRepository.ExistEmail(command.Email))
            {
                return new CommandResult(false, "Email already registered.", null);
            }

            var email = new Email(command.Email);
            var user = new User(email, ERole.Customer);
            user.HasPassword(command.Password);

            await _userRepository.Save(user);

            var token = _tokenService.GenerateToken(user);

            return new CommandResult(true, "Login successfully", new TokenResponseCommand(token));
        }

        public async Task<CommandResult> Handler(ResetPasswordCommand command)
        {
            if (command.Invalid)
            {
                return new CommandResult(false, "Error to find user", command.Notifications);
            }

            var user = await _userRepository.FindByEmail(command.Email);
            return user == null
                ? new CommandResult(false, "user not found.", null)
                : new CommandResult(true, "Generate token", new ResetPasswordCommandResponse(user.Id.ToString(), ""));
        }
    }
}