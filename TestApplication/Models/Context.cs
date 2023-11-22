using Microsoft.EntityFrameworkCore;
using TestApplication.Models.DataModels;

namespace TestApplication.Models
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> opts) : base(opts) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
             .HasIndex(e => e.Email)
             .IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Companies)
                .WithMany(c => c.Employees)
                .UsingEntity(j => j.ToTable("CompanyEmployees"));

            modelBuilder.Entity<Company>()
               .HasMany(e => e.Employees)
               .WithMany(c => c.Companies)
               .UsingEntity(j => j.ToTable("CompanyEmployees"));
        }
    }
}
