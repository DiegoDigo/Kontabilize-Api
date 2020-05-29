using System;
using System.Threading.Tasks;
using FluentValidator;
using Kontabilize.Domain.UserContext.Command.Input;
using Kontabilize.Domain.UserContext.Command.Output;
using Kontabilize.Domain.UserContext.Repositories;
using Kontabilize.Domain.UserContext.Services;
using Kontabilize.Shared.Command;
using Kontabilize.Shared.Handlers;
using Kontabilize.Shared.VOs;

namespace Kontabilize.Domain.UserContext.Handlers
{
     public class ProfileHandler : Notifiable, IHandler<EditProfileCommand>
    {

        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUploadService _uploadService;
        private readonly IAddressRepository _addressRepository;

        public ProfileHandler(IProfileRepository profileRepository, IUserRepository userRepository, IUploadService uploadService, IAddressRepository addressRepository)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _uploadService = uploadService;
            _addressRepository = addressRepository;
        }

        public async Task<CommandResult> GetProfileByIdUser(Guid id)
        {
            var profile = await _profileRepository.GetByUserId(id);
            if (profile == null)
            {
                return new CommandResult(false, "Usuario nao existe", null);
            }

            var result = new GetProfileCommandResponse(profile.Id.ToString(),
                                        profile.Document.Cpf ?? "",
                                        profile.Document.Cnpj ?? "",
                                        profile.Email.Address,
                                        profile.Name.FirstName,
                                        profile.Name.LastName,
                                        profile.Phone.FixNumber,
                                        profile.Phone.MobileNumber,
                                        profile.ImageUrl,
                                        profile.UserId.ToString(),
                                        profile.Address.Id.ToString(),
                                        profile.Address.Street,
                                        profile.Address.City,
                                        profile.Address.Country,
                                        profile.Address.ZipCode
                                        );

            return new CommandResult(true, "Perfil", result);
        }


        public async Task<CommandResult> Handler(EditProfileCommand command)
        {
            if (!command.Validated())
            {
                AddNotifications(command.Notifications);
                return new CommandResult(false, "Erro ao criar o usuario", Notifications);
            }

            var profile = await _profileRepository.GetById(new Guid(command.Id));
            var user = await _userRepository.GetUserById(new Guid(command.UserId));
            var address = await _addressRepository.GetById(new Guid(command.AddressId));

            var email = new Email(command.Email);
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Cpf, command.Cnpj);
            var phone = new Phone(command.FixPhone, command.MobilePhone);

            address = address.EditAddress(command.Street, command.City, command.Country, command.ZipCode);
            await _addressRepository.Update(address);

            
            string imageUrl;
            var image = command.Image;

            if (image != null && image.Length > 0)
            {
                imageUrl = await _uploadService.UploadImageProfile(user.Id, image.OpenReadStream());
            }
            else
            {
                imageUrl = profile.ImageUrl;
            }
            var profileEdit = profile.EditProfile(name, imageUrl, document, email, phone, user, address);
            await _profileRepository.Update(profileEdit);
            return new CommandResult(true, "perfil editado com sucesso", profileEdit);
        }
    }
}