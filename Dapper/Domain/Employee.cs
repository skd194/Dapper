using Dapper.Models;

namespace DDDUsingDapper.Domain
{
    public class Employee
    {

        public Employee(
            int employeeId,
            string name,
            string email,
            string phone,
            string title,
            int companyId) 
        { 
            EmployeeId = employeeId;
            Name = name;
            Email = email;
            Phone = phone;
            Title = title;
            CompanyId = companyId;
        }

        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company{ get; set; }

        public override string ToString()
        {
            return $"{{" +
                        $"Name: {Name}, " +
                        $"Email: {Email}, " +
                        $"Phone: {Phone}, " +
                        $"Title: {Title}, " +
                        $"CompanyId: {CompanyId} " +
                    $"}}";
        }
    }
}
