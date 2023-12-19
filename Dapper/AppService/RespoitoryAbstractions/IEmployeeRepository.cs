using Dapper.Models;
using DDDUsingDapper.Domain;

namespace Dapper.AppService.RespoitoryAbstractions
{
    public interface IEmployeeRepository
    {
        Employee Find(int id);
        IReadOnlyCollection<Employee> GetAll();
        Employee Add(Employee employee);
        void Remove(int id);
        public Employee Update(Employee employee);
    }
}
