using Dapper.Models;

namespace Dapper.AppService.RespoitoryAbstractions
{
    public interface ICompanyRepository
    {
        void Create(Company company);
    }
}
