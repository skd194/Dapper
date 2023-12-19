using Dapper;
using Dapper.AppService.RespoitoryAbstractions;
using DDDUsingDapper.Domain;
using DDDUsingDapper.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DDDUsingDapper.Infrastructure.Repository
{
    public class EmployeeRepositoryUsingDapper : IEmployeeRepository
    {

        private readonly IDbConnection _db;

        public EmployeeRepositoryUsingDapper()
        {
            _db = new SqlConnection(Configuration.ConnectionString);
        }

        public Employee Add(Employee employee)
        {
            var sql = "INSERT INTO Employees " +
                                "(Name, Title, Email, Phone, CompanyId) " +
                        "VALUES  (@Name, @Title, @Email, @Phone, @CompanyId); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT) AS EmployeeId";
            var dapperRow = _db.Query(sql, employee).Single();
            
            var row = new Row(dapperRow);

            employee.EmployeeId = row.GetValue<int>("EmployeeId");

            return employee;
        }

        public Employee Find(int id)
        {
            var sql = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";

            return _db.Query<Employee>(sql, new { @EmployeeId = id }).Single();
        }

        public IReadOnlyCollection<Employee> GetAll()
        {
            var sql = "SELECT * FROM Employees";

            return _db.Query<Employee>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Employees WHERE EmployeeId = @id";

            _db.Execute(sql, new { id });
        }

        public Employee Update(Employee employee)
        {
            var sql = "UPDATE Employees " +
                "SET " +
                    "Name = @Name, " +
                    "Email = @Email, " +
                    "Phone = @Phone, " +
                    "Title = @Title, " +
                    "CompanyId = @CompanyId " +
                 "WHERE EmployeeId = @EmployeeId";

            _db.Query(sql, employee);

            return employee;
        }
    }

    public class Row
    {
        private readonly dynamic _dapperRow;

        public Row(dynamic dapperRow)
        {
            _dapperRow = dapperRow;
        }

        public T GetValue<T>(string columnName)
        {
            var columnValueMap = (IDictionary<string, object>)_dapperRow;
            return (T)columnValueMap[columnName];
        }
    }
}
