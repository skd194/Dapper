using Dapper.AppService;
using Dapper.Models;

internal class Program
{
    private static readonly CompanyService _companySvc = new();


    private static void Main(string[] args)
    {
        Update();

        "Print Item".PrintLine();
        _companySvc.Get(2).PrintLine();
        "Print Collection".PrintLine();
        _companySvc.Get().PrintCollection();
        //CreateCompany();
        //Delete();
    }

    private static void CreateCompany()
    {
        _companySvc.Create("Company X", "ABC", "CITY A", "STATE B", "XXX695");
    }

    private static Company Update()
    {
        var company = new Company(1, "Company 10", "Updated WillWalkers", "UpC", "UpS", "UpPos");
        
        var updatedCompany = _companySvc.Update(company);

        return updatedCompany;
    }

    public static void Delete()
    {
        _companySvc.Delete(3);
    }

}
