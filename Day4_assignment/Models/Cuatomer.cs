namespace Day4_Assignment.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }

        public Customer(string name, string email, string mobileNumber)
        {
            Name = name;
            Email = email;
            MobileNumber = mobileNumber;
        }
    }
}