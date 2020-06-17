using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontabilize.Domain.CompanyContext.Entities;
using Kontabilize.Domain.CompanyContext.Queries;
using Kontabilize.Domain.CompanyContext.Repositories;
using Kontabilize.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Kontabilize.Infra.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly KontabilizeContext _context;

        public CompanyRepository(KontabilizeContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistCnpj(string cnpj) =>
            await _context.Companies.AsNoTracking().AnyAsync(CompanyQuery.FindByCnpj(cnpj));

        public async Task<bool> ExistCpf(string cpf) =>
            await _context.Companies.AsNoTracking().AnyAsync(CompanyQuery.FindByCpf(cpf));

        public async Task<bool> ExistEmail(string email) =>
            await _context.Companies.AsNoTracking().AnyAsync(CompanyQuery.FindByEmail(email));

        public async Task<ICollection<Company>> GetAllNewCompany(int pageNumber, int pageSize) => await _context
            .Companies
            .Where(CompanyQuery.GetAllNewCompany())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        public async Task<ICollection<Company>> GetAllMigrationCompany(int pageNumber, int pageSize) =>
            await _context.Companies
                .Where(CompanyQuery.GetAllMigrationsCompany())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

        public async Task Save(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task<Company> GetCompanyByCpf(string cpf) => await _context.Companies
            .Where(CompanyQuery.FindByCpf(cpf)).AsNoTracking().FirstOrDefaultAsync();

        public async Task<Company> GetCompanyByEmail(string email) => await _context.Companies
            .Where(CompanyQuery.FindByEmail(email)).AsNoTracking().FirstOrDefaultAsync();


        public async Task<Company> GetCompanyByCnpj(string cnpj) => await _context.Companies
            .Where(CompanyQuery.FindByCnpj(cnpj)).AsNoTracking().FirstOrDefaultAsync();

        public async Task<Company> GetById(Guid id) => await _context.Companies.Where(CompanyQuery.FindById(id))
            .AsNoTracking().FirstOrDefaultAsync();

        public async Task Update(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountMigration()
        {
            return await _context.Companies.Where(CompanyQuery.GetAllMigrationsCompany()).CountAsync();
        }

        public async Task<int> CountNew()
        {
            return await _context.Companies.Where(CompanyQuery.GetAllNewCompany()).CountAsync();
        }

        public async Task DeleteCompany(Company company)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }
        
    }
}