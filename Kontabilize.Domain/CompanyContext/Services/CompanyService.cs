using System.Linq;
using System.Threading.Tasks;
using Kontabilize.Domain.CompanyContext.Commands.Output;
using Kontabilize.Domain.CompanyContext.Entities;
using Kontabilize.Domain.CompanyContext.Repositories;
using Kontabilize.Shared.Command;
using Kontabilize.Shared.Command.Output;
using Kontabilize.Shared.VOs;

namespace Kontabilize.Domain.CompanyContext.Services
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CommandResult> GetAllNewCompany(int pageNumber, int pageSize)
        {
            var companies = await _companyRepository.GetAllNewCompany(pageNumber, pageSize);

            if (companies.Count == 0)
            {
                return new CommandResult(false, "Not found companies", null);
            }

            var result = companies.Select(company => new NewCompanyCommandResponse(
                company.Id.ToString(),
                company.Document.Cpf,
                company.Email.Address,
                company.Name.GetFullName(),
                company.Phone.FixNumber,
                company.Phone.MobileNumber,
                company.CompanyTracking,
                company.CreateAt
            )).ToList();

            var totalCompanies = await _companyRepository.CountNew();
            var response = new PagedList<NewCompanyCommandResponse>(result, totalCompanies, pageNumber, pageSize);
            
            return new CommandResult(true, "List new companies", response);
        }


        public async Task<CommandResult> GetNewCompanyByCpf(string cpf)
        {
            var company = await _companyRepository.GetCompanyByCpf(cpf);

            if (company == null)
            {
                return new CommandResult(false, "not found.", null);
            }


            var result = new NewCompanyCommandResponse(
                company.Id.ToString(),
                company.Document.Cpf,
                company.Email.Address,
                company.Name.GetFullName(),
                company.Phone.FixNumber,
                company.Phone.MobileNumber,
                company.CompanyTracking,
                company.CreateAt
            );

            return new CommandResult(true, "new company", result);
        }


        public async Task<CommandResult> GetNewCompanyByEmail(string email)
        {
            var company = await _companyRepository.GetCompanyByEmail(email);
            if (company == null)
            {
                return new CommandResult(false, "not found.", null);
            }

            var result = new NewCompanyCommandResponse(
                company.Id.ToString(),
                company.Document.Cpf,
                company.Email.Address,
                company.Name.GetFullName(),
                company.Phone.FixNumber,
                company.Phone.MobileNumber,
                company.CompanyTracking,
                company.CreateAt
            );

            return new CommandResult(true, "new company", result);
        }

        public async Task<CommandResult> GetAllMigrationsCompany(int pageNumber, int pageSize)
        {
            var companies =  await _companyRepository.GetAllMigrationCompany(pageNumber, pageSize);
            
            if (!companies.Any())
            {
                return new CommandResult(false, "not found", null);
            }
            
            var result = companies.Select(company => new MigrateCompanyCommandResponse(
                company.Id.ToString(),
                company.Document.Cnpj,
                company.Email.Address,
                company.Name.GetFullName(),
                company.Phone.FixNumber,
                company.Phone.MobileNumber,
                company.CompanyTracking,
                company.CreateAt
            )).ToList();

            var totalCompanies = await _companyRepository.CountMigration();
            var response = new PagedList<MigrateCompanyCommandResponse>(result, totalCompanies, pageNumber, pageSize);

            return new CommandResult(true, "migrated companies", response);

        }
        
        public async Task<CommandResult> GetMigrateCompanyByCnpj(string cpf)
        {
            var company = await _companyRepository.GetCompanyByCnpj(cpf);

            if (company == null)
            {
                return new CommandResult(false, "not found.", null);
            }


            var result = new MigrateCompanyCommandResponse(
                company.Id.ToString(),
                company.Document.Cnpj,
                company.Email.Address,
                company.Name.GetFullName(),
                company.Phone.FixNumber,
                company.Phone.MobileNumber,
                company.CompanyTracking,
                company.CreateAt
            );

            return new CommandResult(true, "migrate company", result);
        }


        public async Task<CommandResult> GetMigrateCompanyByEmail(string email)
        {
            var company = await _companyRepository.GetCompanyByEmail(email);
            if (company == null)
            {
                return new CommandResult(false, "not found.", null);
            }

            var result = new MigrateCompanyCommandResponse(
                company.Id.ToString(),
                company.Document.Cnpj,
                company.Email.Address,
                company.Name.GetFullName(),
                company.Phone.FixNumber,
                company.Phone.MobileNumber,
                company.CompanyTracking,
                company.CreateAt
            );

            return new CommandResult(true, "migrate company", result);
        }
        
        
        // public async Task<bool> CreateUserByCompany(Guid id)
        // {
        //     try
        //     {
        //         var company = await _companyRepository.GetById(id);
        //         if (company == null)
        //         {
        //             return false;
        //         }
        //         var user = new User(company.Email, GetCpfOrCnpj(company.Document), ERole.CUSTOMER);
        //
        //         await _userRepository.Save(user);
        //
        //         var profile = new Profile(company.Name, null, company.Document, company.Email, company.Phone, null, null, user, new Address());
        //
        //         await _profileRepository.Save(profile);
        //
        //         await _companyRepository.Update(company.Accept(company));
        //         
        //         return true;
        //     }
        //     catch(Exception ex)
        //     {
        //         return false;
        //     }
        //
        // }

        private static string GetCpfOrCnpj(Document document)        {
            return string.IsNullOrEmpty(document.Cpf) ? document.Cnpj : document.Cpf;
        }

    }
}