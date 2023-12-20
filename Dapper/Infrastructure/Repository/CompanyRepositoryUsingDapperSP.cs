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
    internal class CompanyRepositoryUsingDapperSP : ICompanyRepository
    {
        private readonly IDbConnection _db;

        public CompanyRepositoryUsingDapperSP()
        {
            _db = new SqlConnection(Configuration.ConnectionString);
        }

        public IReadOnlyCollection<Company> GetAll()
        {
            return _db.Query<Company>(
                "usp_GetALLCompany",
                commandType: CommandType.StoredProcedure)
                .ToList();
        }

        public Company Find(int id)
        {
            return _db.Query<Company>(
                "usp_GetCompany",
                new { CompanyId = id },
                commandType: CommandType.StoredProcedure)
                .Single();
        }

        public int Add(Company company)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@CompanyId", 0, DbType.Int32, ParameterDirection.Output);
            parameters.Add("@Name", company.Name);
            parameters.Add("@Address", company.Address);
            parameters.Add("@City", company.City);
            parameters.Add("@State", company.State);
            parameters.Add("@PostalCode", company.PostalCode);

            _db.Execute(
                "usp_AddCompany",
                parameters,
                commandType: CommandType.StoredProcedure);

            company.CompanyId = parameters.Get<int>("@CompanyId");

            return company.CompanyId;
        }

        public Company Update(Company company)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@CompanyId", company.CompanyId);
            parameters.Add("@Name", company.Name);
            parameters.Add("@Address", company.Address);
            parameters.Add("@City", company.City);
            parameters.Add("@State", company.State);
            parameters.Add("@PostalCode", company.PostalCode);

            _db.Execute(
                "usp_UpdateCompany",
                parameters,
                commandType: CommandType.StoredProcedure);

            return company;
        }

        public void Remove(int id)
        {
            _db.Execute(
                "usp_RemoveCompany",
                new { CompanyId = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
