using Dapper.Models;

namespace Dapper.AppService.RespoitoryAbstractions
{
    public interface ICompanyRepository
    {
        Company Find(int id);
        IReadOnlyCollection<Company> GetAll();
        int Add(Company company);
        void Remove(int id);
        public Company Update(Company company);
    }
}
