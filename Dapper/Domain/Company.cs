using Dapper.Contrib.Extensions;
using DDDUsingDapper.Domain;

namespace Dapper.Models
{
    [Table("Companies")]
    public class Company
    {
        public static Company Create(
            string name,
            string address,
            string city,
            string state,
            string postalCode) => new Company(0, name, address, city, state, postalCode);


        public Company()
        {
            Employees = new HashSet<Employee>();
        }

        public Company(
            int companyId,
            string name,
            string address,
            string city,
            string state,
            string postalCode)
            : base()
        {
            CompanyId = companyId;
            Name = name;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
        }

        [Key]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        [Write(false)]
        public HashSet<Employee> Employees { get; set; }

        public void Update(
            string name,
            string address,
            string city,
            string state,
            string postalCode)
        {
            Name = name;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
        }


        public override string ToString()
        {
            return $"{{" +
                        $"Name: {Name}, " +
                        $"Address: {Address}, " +
                        $"City: {City}, " +
                        $"State: {State}, " +
                        $"PostalCode: {PostalCode} " +
                        $"Employees: {Employees.ToStringCollection()}" +
                    $"}}";
        }

        
    }
}
