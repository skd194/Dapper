namespace Dapper.Models
{
    public class Company
    {
        public static Company Create(
            string name,
            string address,
            string city,
            string state,
            string postalCode) => new Company(0, name, address, city, state, postalCode);


        protected Company()
        {
        }

        private Company(
            int companyId,
            string name,
            string address,
            string city,
            string state,
            string postalCode)
        {
            CompanyId = companyId;
            Name = name;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
        }


        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

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
    }
}
