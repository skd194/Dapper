using Dapper.AppService;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("1. Create Company");
        CreateCompany();
    }

    private static void CreateCompany()
    {
       var companyService = new CompanyService();
       companyService.Create("Company X", "ABC", "CITY A", "STATE B", "XXX695");
    }
}