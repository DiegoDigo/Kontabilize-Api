using System.Reflection;
using Kontabilize.Domain.CompanyContext.Entities;
using Kontabilize.Domain.UserContext.Entities;
using Kontabilize.Domain.UserContext.Entities.Enums;
using Kontabilize.Infra.Extensions;
using Kontabilize.Infra.Mappers;
using Kontabilize.Shared.VOs;
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
            optionsBuilder.UseNpgsql(
                @"Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=kontabilize;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Seed();
            
            base.OnModelCreating(modelBuilder);
        }

        
    }
}