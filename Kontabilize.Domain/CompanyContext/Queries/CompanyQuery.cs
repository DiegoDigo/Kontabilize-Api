using System;
using System.Linq.Expressions;
using Kontabilize.Domain.CompanyContext.Entities;
using Kontabilize.Domain.CompanyContext.Entities.enums;

namespace Kontabilize.Domain.CompanyContext.Queries
{
    public static class CompanyQuery
    {
        public static Expression<Func<Company, bool>> FindByEmail(string email)
        {
            return x => x.Email.Address == email && (x.TypeCompany == ETypeCompany.NewCompany || x.TypeCompany == ETypeCompany.MigrateCompany);
        }

        public static Expression<Func<Company, bool>> FindByCpf(string cpf)
        {
            return x => x.Document.Cpf == cpf && x.TypeCompany == ETypeCompany.NewCompany;
        }

        public static Expression<Func<Company, bool>> FindByCnpj(string cnpj)
        {
            return x => x.Document.Cnpj == cnpj && x.TypeCompany == ETypeCompany.MigrateCompany;
        }

        public static Expression<Func<Company, bool>> GetAllNewCompany()
        {
            return x => x.TypeCompany == ETypeCompany.NewCompany;
        }

        public static Expression<Func<Company, bool>> GetAllMigrationsCompany()
        {
            return x => x.TypeCompany == ETypeCompany.MigrateCompany;
        }
        
        public static Expression<Func<Company, bool>> FindById(Guid id)
        {
            return x => x.Id == id;
        }
    }
}