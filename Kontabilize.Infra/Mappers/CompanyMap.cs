using Kontabilize.Domain.CompanyContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kontabilize.Infra.Mappers
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Document, document =>
            {
                document.Property(x => x.Cnpj).HasColumnName("Cnpj").HasColumnType("varchar(14)");
                document.Property(x => x.Cpf).HasColumnName("Cpf").HasColumnType("varchar(12)");
            });
            builder.OwnsOne(x => x.Email,
                email =>
                {
                    email.Property(x => x.Address).HasColumnName("Email").HasColumnType("varchar(160)").IsRequired();
                });
            builder.Property(x => x.CreateAt).IsRequired().HasColumnType("timestamp");
            builder.Property(x => x.CompanyTracking).IsRequired().HasColumnType("varchar(120)");
            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(x => x.FirstName).HasColumnName("First_Name").IsRequired().HasColumnType("varchar(60)");
                name.Property(x => x.LastName).HasColumnName("Last_Name").IsRequired().HasColumnType("varchar(60)");
            });
            builder.OwnsOne(x => x.Phone, phone =>
            {
                phone.Property(x => x.FixNumber).HasColumnName("Fix_Number").IsRequired().HasColumnType("varchar(10)");
                phone.Property(x => x.MobileNumber).HasColumnName("Mobile_Number").IsRequired()
                    .HasColumnType("varchar(11)");
            });

            builder.Property(x => x.TypeCompany).HasConversion<string>();
        }
    }
}