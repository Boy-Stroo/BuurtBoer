namespace Web_App.Data
{
    public class Users
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public Users()
        {

        }

        public Users(Guid id, string firstName, string lastName, string emailAddress, string password, Role role)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Password = password;
            Role = role;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {FirstName} {LastName}, Email: {EmailAddress}, Role: {Role}";
        }
    }

    public enum Role
    {
        SuperAdmin,
        Admin,
        Employee
    }
}
