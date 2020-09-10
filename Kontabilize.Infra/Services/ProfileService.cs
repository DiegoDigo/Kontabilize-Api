using System;
using System.Threading.Tasks;
using Kontabilize.Domain.UserContext.Command.Output;
using Kontabilize.Domain.UserContext.Repositories;
using Kontabilize.Domain.UserContext.Services;
using Kontabilize.Shared.Command;

namespace Kontabilize.Infra.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<CommandResult> GetProfileByIdUser(Guid id)
        {
            var profile = await _profileRepository.GetByUserId(id);
            if (profile == null)
            {
                return new CommandResult(false, "Profile not found.", null);
            }

            var result = new ProfileCommandResponse(profile.Id,
                profile.Document.Cpf ?? "",
                profile.Document.Cnpj ?? "",
                profile.Email.Address,
                profile.Name.FirstName,
                profile.Name.LastName,
                profile.Phone.FixNumber,
                profile.Phone.MobileNumber,
                profile.ImageUrl,
                profile.UserId,
                profile.Address.Id,
                profile.Address.Street,
                profile.Address.City,
                profile.Address.Country,
                profile.Address.ZipCode
            );

            return new CommandResult(true, "Perfil", result);
        }
    }
}