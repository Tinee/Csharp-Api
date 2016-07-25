namespace Contracts
{
    public abstract class BaseUser
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string Adress { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }

        public int OfficeId { get; set; }
    }

    public class Employee : BaseUser
    {
        public int EmployeeId { get; set; }

        public string Username { get; set; }
    }

    public class Customer 
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }
    }
}
