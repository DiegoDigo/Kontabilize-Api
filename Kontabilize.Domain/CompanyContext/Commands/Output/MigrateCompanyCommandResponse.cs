using System;

namespace Kontabilize.Domain.CompanyContext.Commands.Output
{
    public class MigrateCompanyCommandResponse
    {
        public string Id { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FixPhone { get; set; }
        public string MobilePhone { get; set; }
        public string CompanyTracking { get; set; }
        public DateTime CreateAt { get; set; }

        public MigrateCompanyCommandResponse(string id, string cnpj, string email, string name, string fixPhone,
            string mobilePhone, string companyTracking, DateTime createAt)
        {
            Id = id;
            Cnpj = cnpj;
            Email = email;
            Name = name;
            FixPhone = fixPhone;
            MobilePhone = mobilePhone;
            CompanyTracking = companyTracking;
            CreateAt = createAt;
        }
    }
}