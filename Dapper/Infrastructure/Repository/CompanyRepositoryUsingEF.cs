using Dapper.AppService.RespoitoryAbstractions;
using Dapper.Infrastructure.Data;
using Dapper.Models;

namespace Dapper.Infrastructure.Repository
{
    internal class CompanyRepositoryUsingEF : ICompanyRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public CompanyRepositoryUsingEF()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IReadOnlyCollection<Company> GetAll()
        {
            throw new NotImplementedException();
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

        public Company Find(int id)
        {
            throw new NotImplementedException();
        }

        int ICompanyRepository.Add(Company company)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Company Update(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
