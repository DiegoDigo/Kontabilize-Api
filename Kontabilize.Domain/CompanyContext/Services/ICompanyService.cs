using System;
using System.Threading.Tasks;
using Kontabilize.Shared.Command;

namespace Kontabilize.Domain.CompanyContext.Services
{
    public interface ICompanyService
    {
        Task<CommandResult> GetAllNewCompany(int pageNumber, int pageSize);
        Task<CommandResult> GetNewCompanyByCpf(string cpf);
        Task<CommandResult> GetNewCompanyByEmail(string email);
        Task<CommandResult> GetAllMigrationsCompany(int pageNumber, int pageSize);
        Task<CommandResult> GetMigrateCompanyByCnpj(string cnpj);
        Task<CommandResult> GetMigrateCompanyByEmail(string email);
        Task<bool> DeleteCompany(Guid id);
        Task<CommandResult> GetAll(int pageNumber, int pageSize);
    }
}