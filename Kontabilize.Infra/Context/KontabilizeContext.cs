using Kontabilize.Domain.UserContext.Entities;
using Kontabilize.Infra.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Kontabilize.Infra.Context
{
    public class KontabilizeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=Kontabilize;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}