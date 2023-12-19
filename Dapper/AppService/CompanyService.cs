using Dapper.AppService.RespoitoryAbstractions;
using Dapper.Models;
using DDDUsingDapper.Infrastructure.Repository;

namespace Dapper.AppService
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService()
        {
            //_companyRepository = new CompanyRepositoryUsingEF();
            _companyRepository = new CompanyRepositoryUsingDapper();
        }

        public IReadOnlyCollection<Company> Get()
        {
            return _companyRepository.GetAll(); 
        }

        public Company Get(int id)
        {
            return _companyRepository.Find(id);
        }

        public int Create(
            string name, 
            string address, 
            string city, 
            string state, 
            string postalCode)  
        {
            var company = Company.Create(name, address, city, state, postalCode);
            return _companyRepository.Add(company);
        }

        public Company Update(Company company)
        {
            return _companyRepository.Update(company);
        }

        public void Delete(int id)
        {
            _companyRepository.Remove(id);
        }
    }
}
