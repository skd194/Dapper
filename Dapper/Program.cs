using Dapper.AppService;
using Dapper.Models;
using DDDUsingDapper.Domain;

internal class Program
{
    private static readonly CompanyService _companySvc = new();
    private static readonly EmployeeService _empSvc = new();

    private static void Main(string[] args)
    {
        Update();
        "Print Item".PrintLine();
        _companySvc.Get(2).PrintLine();
        "Print Collection".PrintLine();
        _companySvc.Get().PrintCollection();
        CreateCompany();
        Delete();

        "Employee".PrintLine();
        UpdateEmployee();
        //CreateEmployee().PrintLine();
        DeleteEmployee(4);
        "Print Item".PrintLine();
        _empSvc.Get(2).PrintLine();
        "Print Collection".PrintLine();
        _empSvc.Get().PrintCollection();

        
    }

    private static void CreateCompany()
    {
        _companySvc.Create("Company X", "ABC", "CITY A", "STATE B", "XXX695");
    }

    private static Company Update()
    {
        var company = new Company(4, "Company 10", "Updated WillWalkers", "UpC", "UpS", "UpPos");
        
        var updatedCompany = _companySvc.Update(company);

        return updatedCompany;
    }

    public static void Delete()
    {
        _companySvc.Delete(4);
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
}
