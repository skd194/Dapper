using Dapper.AppService;
using Dapper.Models;
using DDDUsingDapper.AppService.RespoitoryAbstractions;
using DDDUsingDapper.Domain;
using DDDUsingDapper.Infrastructure.Repository;

internal class Program
{
    private static readonly CompanyService _companySvc = new();
    private static readonly EmployeeService _empSvc = new();
    private static readonly IAggregateRepository _aggRepo = new AggregateRepository();


    private static void Main(string[] args)
    {
        //Update(6);
        //"Print Item".PrintLine();
        //_companySvc.Get(2).PrintLine();
        //"Print Collection".PrintLine();
        //_companySvc.Get().PrintCollection();
        //CreateCompany();
        //Delete(5);

        //"Employee".PrintLine();
        //UpdateEmployee();
        //CreateEmployee().PrintLine();
        //DeleteEmployee(4);
        //"Print Item".PrintLine();
        //_empSvc.Get(2).PrintLine();
        //"Print Collection".PrintLine();
        //_empSvc.Get().PrintCollection();
        "--------------------Employee By Company-----------------------".PrintLine();
        // GetEmployeeByCompany(0).PrintCollection();

        "--------------------Company With Employee-----------------------".PrintLine();
        // GetCompanyWithEmployees(1).PrintLine();
        $@"*****
        ******************* GetAllCompanyWithEmployees ****************
        *****".PrintLine();
        // GetAllCompanyWithEmployees().PrintCollection();
        $"************ADD EMPLOYEE*******************************".PrintLine();
        AddCompanyWithEmployees();

        RemoveCompanies();
    }

    private static void CreateCompany()
    {
        _companySvc.Create("Company X", "ABC", "CITY A", "STATE B", "XXX695");
    }

    private static Company Update(int id)
    {
        var company = new Company(id, "Company 10", "Updated WillWalkers", "UpC", "UpS", "UpPos");
        
        var updatedCompany = _companySvc.Update(company);

        return updatedCompany;
    }

    public static void Delete(int id)
    {
        _companySvc.Delete(id);
    }


    public static Employee CreateEmployee() 
    {
        return _empSvc.Create("John", "johndx@gmail.com", "45464654", "atdg", 1);
    }

    private static Employee UpdateEmployee()
    {
        var emp = new Employee(1, "Emp 10", "Updated WillWalkers", "UpC", "UpS", 2);

        var updatedEmployee = _empSvc.Update(emp);

        return updatedEmployee;
    }

    public static void DeleteEmployee(int id)
    {
        _empSvc.Delete(id);
    }

    public static IReadOnlyCollection<Employee> GetEmployeeByCompany(int companyId)
    {
        return _aggRepo.GetEmployeesByCompany(companyId);
    }

    public static Company GetCompanyWithEmployees(int companyId)
    {
        return _aggRepo.GetCompanyWithEmployees(companyId);
    }

    public static IReadOnlyCollection<Company> GetAllCompanyWithEmployees()
    {
        return _aggRepo.GetAllCompanyWithEmployees();
    }


    public static void AddCompanyWithEmployees()
    {
        var company = Company.Create("ABC CO", "ADD", "CITY", "STATE", "PC");
        company.Employees = new HashSet<Employee>
        {
            new Employee(0, "A", "a@mail.com", "24242424", "titleA", 0),
            new Employee(0, "B", "b@mail.com", "65656565", "titleB", 0)
        };

        _aggRepo.AddCompanyWithEmployees(company);
    }

    public static void RemoveCompanies()
    {
        var companies = _aggRepo.FilterCompanyByName("Company");

        _aggRepo.RemoveRange(companies.Select(x => x.CompanyId).ToArray());
    }
}
