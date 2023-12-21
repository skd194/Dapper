using Dapper.Models;
using DDDUsingDapper.Domain;

namespace DDDUsingDapper.AppService.RespoitoryAbstractions
{
    internal interface IAggregateRepository
    {
        IReadOnlyCollection<Employee> GetAllEmployees();
        IReadOnlyCollection<Employee> GetEmployeesByCompany(int companyId);
        Company GetCompanyWithEmployees(int id);
        IReadOnlyCollection<Company> GetAllCompanyWithEmployees();
        void AddCompanyWithEmployees(Company company);
        void RemoveRange(int[] companyIds);
        IReadOnlyCollection<Company> FilterCompanyByName(string name);
    }
}
