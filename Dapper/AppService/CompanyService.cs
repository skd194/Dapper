using Dapper.AppService.RespoitoryAbstractions;
using Dapper.Infrastructure.Repository;
using Dapper.Models;

namespace Dapper.AppService
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService()
        {
            _companyRepository = new CompanyRepository();
        }

        public void Create(
            string name, 
            string address, 
            string city, 
            string state, 
            string postalCode)  
        {
            var company = Company.Create(name, address, city, state, postalCode);
            _companyRepository.Create(company);
        }


    }
}
