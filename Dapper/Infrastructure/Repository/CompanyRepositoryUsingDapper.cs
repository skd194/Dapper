using Dapper;
using Dapper.AppService.RespoitoryAbstractions;
using Dapper.Models;
using DDDUsingDapper.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Xml.Linq;

namespace DDDUsingDapper.Infrastructure.Repository
{
    internal class CompanyRepositoryUsingDapper : ICompanyRepository
    {
        private readonly IDbConnection _db;

        public CompanyRepositoryUsingDapper()
        {
            _db = new SqlConnection(Configuration.ConnectionString);
        }

        public IReadOnlyCollection<Company> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            
            return _db.Query<Company>(sql).ToList();
        }

        public Company Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId";
            
            return _db.Query<Company>(sql, new { @CompanyId = id }).Single();
        }

        public int Add(Company company)
        {
            var sql = "INSERT INTO Companies" +
                        "(Name,  Address,  City,  State,  PostalCode)" +
                "VALUES  (@Name, @Address, @City, @State, @PostalCode);" +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";
            
            return _db.Query<int>(sql, company).Single();
        }

        public Company Update(Company company)
        {
            var sql = "UPDATE Companies " +
                "SET " +
                    "Name = @Name, " +
                    "Address = @Address, " +
                    "City = @City, " +
                    "State = @State, " +
                    "PostalCode = @PostalCode " +
                "WHERE CompanyId = @CompanyId";

            _db.Execute(sql, company);

            return company;
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Companies WHERE CompanyId = @id";

            _db.Execute(sql, new { id });
        }
    }
}
