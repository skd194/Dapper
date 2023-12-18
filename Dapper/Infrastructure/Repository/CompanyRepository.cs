using Dapper.AppService.RespoitoryAbstractions;
using Dapper.Infrastructure.Data;
using Dapper.Models;

namespace Dapper.Infrastructure.Repository
{
    internal class CompanyRepository : ICompanyRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public CompanyRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public void Create(Company company)
        {
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
