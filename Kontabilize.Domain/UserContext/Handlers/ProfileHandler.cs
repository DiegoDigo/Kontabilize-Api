using System;
using System.Threading.Tasks;
using FluentValidator;
using Kontabilize.Domain.UserContext.Command.Input;
using Kontabilize.Domain.UserContext.Command.Output;
using Kontabilize.Domain.UserContext.Entities;
using Kontabilize.Domain.UserContext.Repositories;
using Kontabilize.Domain.UserContext.Services;
using Kontabilize.Shared.Command;
using Kontabilize.Shared.Handlers;
using Kontabilize.Shared.VOs;

namespace Kontabilize.Domain.UserContext.Handlers
{
    public class ProfileHandler : IHandler<EditProfileCommand>, IHandler<CreateProfileCommand>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUploadService _uploadService;
        private readonly IAddressRepository _addressRepository;

        public ProfileHandler(IProfileRepository profileRepository, IUserRepository userRepository,
            IUploadService uploadService, IAddressRepository addressRepository)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _uploadService = uploadService;
            _addressRepository = addressRepository;
        }

        


        public async Task<CommandResult> Handler(EditProfileCommand command)
        {
            if (!command.Validated())
            {
                return new CommandResult(false, "Error editing profile", command.Notifications);
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
            
            var response = new ProfileCommandResponse(
                profile.Id,
                profile.Document.Cpf,
                profile.Document.Cnpj,
                profile.Email.Address,
                profile.Name.FirstName,
                profile.Name.LastName,
                profile.Phone.FixNumber,
                profile.Phone.MobileNumber,
                profile.ImageUrl,
                profile.User.Id,
                profile.Address.Id,
                profile.Address.Street,
                profile.Address.City,
                profile.Address.Country,
                profile.Address.ZipCode
            );
            
            return new CommandResult(true, "perfil editado com sucesso", response);
        }


        public async Task<CommandResult> Handler(CreateProfileCommand command)
        {
            if (!command.Validated())
            {
                return new CommandResult(false, "Error editing profile", command.Notifications);
            }

            var user = await _userRepository.GetUserById(new Guid(command.UserId));

            if (user == null)
            {
                return new CommandResult(false, "User not found.", null);
            }

            var address = new Address(command.Street, command.City, command.Country, command.ZipCode);

            var email = new Email(command.Email);
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Cpf, command.Cnpj);
            var phone = new Phone(command.FixPhone, command.MobilePhone);

            await _addressRepository.Save(address);

            var imageUrl = "";
            var image = command.Image;

            if (command.Image != null && command.Image.Length > 0)
            {
                imageUrl = await _uploadService.UploadImageProfile(user.Id, image.OpenReadStream());
            }

            var profile = new Profile(name, imageUrl, document, email, phone, user, address);
            await _profileRepository.Save(profile);

            var response = new ProfileCommandResponse(
                profile.Id,
                profile.Document.Cpf,
                profile.Document.Cnpj,
                profile.Email.Address,
                profile.Name.FirstName,
                profile.Name.LastName,
                profile.Phone.FixNumber,
                profile.Phone.MobileNumber,
                profile.ImageUrl,
                profile.User.Id,
                profile.Address.Id,
                profile.Address.Street,
                profile.Address.City,
                profile.Address.Country,
                profile.Address.ZipCode
            );
            return new CommandResult(true, "Profile created.", response);
        }
    }
}