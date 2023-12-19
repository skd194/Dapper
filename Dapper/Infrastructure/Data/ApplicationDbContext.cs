using Dapper.Models;
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
    }
}
