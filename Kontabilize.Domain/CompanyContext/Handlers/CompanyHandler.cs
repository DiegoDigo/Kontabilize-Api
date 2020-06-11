using System.Threading.Tasks;
using FluentValidator;
using Kontabilize.Domain.CompanyContext.Commands.Inputs;
using Kontabilize.Domain.CompanyContext.Commands.Output;
using Kontabilize.Domain.CompanyContext.Entities;
using Kontabilize.Domain.CompanyContext.Entities.enums;
using Kontabilize.Domain.CompanyContext.Repositories;
using Kontabilize.Shared.Command;
using Kontabilize.Shared.Handlers;
using Kontabilize.Shared.VOs;

namespace Kontabilize.Domain.CompanyContext.Handlers
{
    public class CompanyHandler : Notifiable, IHandler<CreateMigrateCompanyCommand>, IHandler<CreateNewCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CommandResult> Handler(CreateMigrateCompanyCommand command)
        {
            if (!command.Validated())
            {
                return new CommandResult(false, "Error migrate company", command.Notifications);
            }

            if (await _companyRepository.ExistCnpj(command.Cnpj))
            {
                AddNotification("Cnpj", "Cnpj already registered");
                return new CommandResult(false, "Error migrate company", Notifications);
            }


            if (await _companyRepository.ExistEmail(command.Email))
            {
                AddNotification("Email", "Email already registered");
                return new CommandResult(false, "Error migrate company", Notifications);
            }

            var company = new Company(
                new Document().DocumentCnpj(command.Cnpj),
                new Email(command.Email),
                new Name(command.FirstName, command.LastName),
                new Phone(command.FixPhone, command.MobilePhone),
                command.CompanyTracking,
                ETypeCompany.MigrateCompany
            );

            await _companyRepository.Save(company);

            var response = new MigrateCompanyCommandResponse(
                company.Id.ToString(),
                company.Document.Cnpj,
                company.Email.Address,
                company.Name.GetFullName(),
                company.Phone.FixNumber,
                company.Phone.MobileNumber,
                company.CompanyTracking,
                company.CreateAt);

            return new CommandResult(true, "migrated company successfully.", response);
            
        }

        public async Task<CommandResult> Handler(CreateNewCompanyCommand command)
        {
            if (!command.Validated())
            {
                return new CommandResult(false, "Error creating new company", command.Notifications);
            }

            if (await _companyRepository.ExistCpf(command.Cpf))
            {
                AddNotification("Cpf", "Cpf already registered");
                return new CommandResult(false, "Error creating new company", Notifications);
            }


            if (await _companyRepository.ExistEmail(command.Email))
            {
                AddNotification("Email", "Email already registered");
                return new CommandResult(false, "Error creating new company", Notifications);
            }

            var company = new Company(
                new Document().DocumentCpf(command.Cpf),
                new Email(command.Email),
                new Name(command.FirstName, command.LastName),
                new Phone(command.FixPhone, command.MobilePhone),
                command.CompanyTracking,
                ETypeCompany.NewCompany
            );

            await _companyRepository.Save(company);

            var response = new NewCompanyCommandResponse(
                company.Id.ToString(),
                company.Document.Cpf,
                company.Email.Address,
                company.Name.GetFullName(),
                company.Phone.FixNumber,
                company.Phone.MobileNumber,
                company.CompanyTracking,
                company.CreateAt);

            return new CommandResult(true, "company successfully registered", response);
        }
    }
}