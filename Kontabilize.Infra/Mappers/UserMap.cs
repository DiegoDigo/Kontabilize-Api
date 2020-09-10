using Kontabilize.Domain.UserContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kontabilize.Infra.Mappers
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Email, email => {
                email.Property(x => x.Address).IsRequired().HasColumnName("Email").HasColumnType("varchar(160)");
                email.HasIndex(x => x.Address);
            });
            builder.Property(x => x.Password).IsRequired().HasColumnType("varchar(60)");
            builder.Property(x => x.CreateAt).IsRequired().HasColumnType("timestamp");
            builder.Property(x => x.Active).IsRequired().HasColumnType("boolean");
            builder.Property(x => x.Role).HasConversion<int>();
        }
    }
}