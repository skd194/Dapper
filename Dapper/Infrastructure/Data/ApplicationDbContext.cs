using Dapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Dapper.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private const string ConnectionString = "Server=DESKTOP-845U4LI;Database=Dapper;Trusted_Connection=True;MultipleActiveResultSets=true;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<Company> Companies { get; set; }
    }
}
