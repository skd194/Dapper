using Dapper.Models;
using DDDUsingDapper.Domain;
using DDDUsingDapper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dapper.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Company>().Ignore(t => t.Employees);

            builder.Entity<Employee>()
                .HasOne(c => c.Company)
                .WithMany()
                .HasForeignKey(c => c.CompanyId);
        }
    }
}
