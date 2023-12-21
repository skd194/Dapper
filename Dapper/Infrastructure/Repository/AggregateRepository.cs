using Dapper;
using Dapper.AppService.RespoitoryAbstractions;
using Dapper.Models;
using DDDUsingDapper.AppService.RespoitoryAbstractions;
using DDDUsingDapper.Domain;
using DDDUsingDapper.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace DDDUsingDapper.Infrastructure.Repository
{
    internal class AggregateRepository : IAggregateRepository
    {

        private readonly IDbConnection _db;

        public AggregateRepository()
        {
            _db = new SqlConnection(Configuration.ConnectionString);
        }

        public void AddCompanyWithEmployees(Company company)
        {
            using var trasaction = new TransactionScope();
            try
            {
                var sql = "INSERT INTO Companies" +
                        "(Name,  Address,  City,  State,  PostalCode)" +
                "VALUES  (@Name, @Address, @City, @State, @PostalCode);" +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                var id = _db.Query<int>(sql, company).Single();

                company.CompanyId = id;

                foreach (var employee in company.Employees)
                {
                    employee.CompanyId = id;
                }

                var empInsertSql = "INSERT INTO Employees " +
                            "(Name, Title, Email, Phone, CompanyId) " +
                    "VALUES  (@Name, @Title, @Email, @Phone, @CompanyId); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT) AS EmployeeId";

                _db.Execute(empInsertSql, company.Employees);

                trasaction.Complete();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            
        }

        public IReadOnlyCollection<Company> GetAllCompanyWithEmployees()
        {
            var sql = @"SELECT c.*, e.*
                        FROM Companies AS c 
                        INNER JOIN Employees AS e 
                            ON c.CompanyId = e.CompanyId";

            var companyMap = new Dictionary<int, Company>();


            var companies = _db.Query<Company, Employee, Company>(
                sql,
                (c, e) =>
                {
                    if (companyMap.TryGetValue(c.CompanyId, out var company))
                    {
                        company.Employees.Add(e);
                    }
                    else
                    {
                        company = c;
                        company.Employees.Add(e);
                        companyMap.Add(c.CompanyId, company);
                    }
                    return company;
                }, splitOn: "EmployeeId");

            return companies.ToList();

        }

        public IReadOnlyCollection<Employee> GetAllEmployees()
        {
            //var sql = @"SELECT 
            //                e.*,
            //                c.* 
            //            FROM Employees e
            //            JOIN Companies c
            //            ON e.CompanyId = c.CompanyId";
            var sql = @"SELECT 
                            e.EmployeeId,
                            e.Name,
                            e.Email,
                            e.Phone,
                            e.Title,
                            c.CompanyId,
                            c.Name,
                            c.Address,
                            c.City,
                            c.State,
                            c.PostalCode 
                        FROM Employees e
                        JOIN Companies c
	                    ON e.CompanyId = c.CompanyId";

            var employees = _db.Query<Employee, Company, Employee>(
                sql,
                (emp, cmp) =>
                {
                    emp.Company = cmp;
                    return emp;
                },
                splitOn: "CompanyId");

            return employees.ToList();
        }

        public Company GetCompanyWithEmployees(int id)
        {
            var param = new { CompanyId = id };

            var sql = "SELECT * FROM Companies WHERE CompanyId = @companyId;" +
                " SELECT * FROM Employees WHERE CompanyId = @companyId;";

            Company company;

            using var resultSet = _db.QueryMultiple(sql, param);
            company = resultSet.Read<Company>().Single();
            company.Employees = resultSet.Read<Employee>().ToHashSet();

            return company;
        }

        public IReadOnlyCollection<Employee> GetEmployeesByCompany(int companyId)
        {
            var sql = @"SELECT 
                            e.*,
                            c.* 
                        FROM Employees e
                        JOIN Companies c
                        ON e.CompanyId = c.CompanyId";

            if (companyId != 0)
            {
                sql += " WHERE e.CompanyId = @companyId";
            }

            var employees = _db.Query<Employee, Company, Employee>(
                sql,
                (emp, cmp) =>
                {
                    emp.Company = cmp;
                    return emp;
                },
                new { companyId },
                splitOn: "CompanyId");

            return employees.ToList();
        }


        public void RemoveRange(int[] companyIds)
        {
            _db.Query("DELETE FROM Companies WHERE CompanyId IN @companyIds", new { companyIds });
        }


        public IReadOnlyCollection<Company> FilterCompanyByName(string name)
        {
            var sql = "SELECT * FROM Companies WHERE Name LIKE '%' + @name + '%'";

            return _db.Query<Company>(sql, new { name }).ToList();
        }

    }
}
