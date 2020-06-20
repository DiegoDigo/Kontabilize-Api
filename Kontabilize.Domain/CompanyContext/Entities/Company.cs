using System;
using Kontabilize.Domain.CompanyContext.Entities.enums;
using Kontabilize.Shared.Entities;
using Kontabilize.Shared.VOs;

namespace Kontabilize.Domain.CompanyContext.Entities
{
    public class Company : Entity
    {
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public DateTime CreateAt { get; private set; }
        public Name Name { get; private set; }
        public Phone Phone { get; private set; }
        public string CompanyTracking { get; private set; }
        public ETypeCompany TypeCompany { get; private set; }

        public bool Actived { get; private set; }
        
        public Company()
        {
        }

        public Company(Document document, Email email, Name name, Phone phone, string companyTracking,
            ETypeCompany typeCompany)
        {
            Document = document;
            Email = email;
            Name = name;
            Phone = phone;
            CompanyTracking = companyTracking;
            TypeCompany = typeCompany;
            CreateAt = DateTime.Now;
            Actived = false;
        }


        public void Active()
        {
            Actived = true;
        }
    }
}