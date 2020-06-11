using System;

namespace Kontabilize.Domain.CompanyContext.Commands.Output
{
    public class NewCompanyCommandResponse
    {
        public string Id { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FixPhone { get; set; }
        public string MobilePhone { get; set; }
        public string CompanyTracking { get; set; }
        public DateTime CreateAt { get; set; }

        public NewCompanyCommandResponse(string id, string cpf, string email, string name, string fixPhone,
            string mobilePhone, string companyTracking, DateTime createAt)
        {
            Id = id;
            Cpf = cpf;
            Email = email;
            Name = name;
            FixPhone = fixPhone;
            MobilePhone = mobilePhone;
            CompanyTracking = companyTracking;
            CreateAt = createAt;
        }
    }
}