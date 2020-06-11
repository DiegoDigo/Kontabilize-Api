using Kontabilize.Domain.CompanyContext.Entities;
using Kontabilize.Domain.UserContext.Entities;
using Kontabilize.Infra.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Kontabilize.Infra.Context
{
    public class KontabilizeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        
        public DbSet<Company> Companies { get; set; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=kontabilize;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProfileMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}