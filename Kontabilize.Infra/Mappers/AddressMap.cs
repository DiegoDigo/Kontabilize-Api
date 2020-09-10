using Kontabilize.Domain.UserContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kontabilize.Infra.Mappers
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Street).HasColumnType("varchar(150)");
            builder.Property(x => x.City).HasColumnType("varchar(50)");
            builder.Property(x => x.Country).HasColumnType("varchar(50)");
            builder.Property(x => x.ZipCode).HasColumnType("varchar(8)");
            builder.HasIndex(x => x.ZipCode);
        }
    }
}