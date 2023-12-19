using Dapper.AppService.RespoitoryAbstractions;
using Dapper.Models;
using DDDUsingDapper.Domain;
using DDDUsingDapper.Infrastructure.Repository;

namespace Dapper.AppService
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepositoryUsingDapper();
        }

        public IReadOnlyCollection<Employee> Get()
        {
            return _employeeRepository.GetAll(); 
        }

        public Employee Get(int id)
        {
            return _employeeRepository.Find(id);
        }

        public Employee Create(
            string name, 
            string email, 
            string phone, 
            string title, 
            int companyId)  
        {
            var employee = new Employee(0, name, email, phone, title, companyId);
            return _employeeRepository.Add(employee);
        }

        public Employee Update(Employee company)
        {
            return _employeeRepository.Update(company);
        }

        public void Delete(int id)
        {
            _employeeRepository.Remove(id);
        }
    }
}
