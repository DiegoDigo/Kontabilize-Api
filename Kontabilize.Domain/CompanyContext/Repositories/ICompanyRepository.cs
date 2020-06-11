using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontabilize.Domain.CompanyContext.Entities;

namespace Kontabilize.Domain.CompanyContext.Repositories
{
    public interface ICompanyRepository
    {
        Task Save(Company company);
        Task<bool> ExistEmail(string email);
        Task<bool> ExistCpf(string cpf);
        Task<bool> ExistCnpj(string cnpj);
        Task<Company> GetById(Guid id);
        Task<Company> GetCompanyByCpf(string cpf);
        Task<Company> GetCompanyByCnpj(string cnpj);
        Task<Company> GetCompanyByEmail(string email);
        Task<ICollection<Company>> GetAllNewCompany();
        Task<ICollection<Company>> GetAllMigrationCompany();
        Task Update(Company company);
    }
}