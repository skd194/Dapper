using Dapper;
using Dapper.AppService.RespoitoryAbstractions;
using Dapper.Contrib.Extensions;
using Dapper.Models;
using DDDUsingDapper.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Xml.Linq;

namespace DDDUsingDapper.Infrastructure.Repository
{
    internal class CompanyRepositoryUsingDapperContrib : ICompanyRepository
    {
        private readonly IDbConnection _db;

        public CompanyRepositoryUsingDapperContrib()
        {
            _db = new SqlConnection(Configuration.ConnectionString);
        }

        public IReadOnlyCollection<Company> GetAll()
        {
            return _db.GetAll<Company>().ToList();
        }

        public Company Find(int id)
        {
           return _db.Get<Company>(id); 
        }

        public int Add(Company company)
        {
            var id = _db.Insert(company);
            
            company.CompanyId = (int)id;

            return company.CompanyId;
        }

        public Company Update(Company company)
        {

            _db.Update(company);

            return company;
        }

        public void Remove(int id)
        {
            _db.Delete(new Company { CompanyId = id});
        }
    }
}
