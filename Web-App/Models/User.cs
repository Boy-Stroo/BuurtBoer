using System.Text.Json.Serialization;

namespace Web_App
{
    public enum Role
    {
        SuperAdmin,
        Admin,
        Employee
    }
    public class User
    {
        private string _email;
        public Guid Id { get; init; }
        //[JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        //[JsonPropertyName("lastName")]
        public string LastName { get; set; }
        //[JsonPropertyName("password")]

        public string Password { get; set; }
        //[JsonPropertyName("email")]
        public string Email
        {
            get => _email;
            set => _email = value.ToLower();
        }
        //[JsonPropertyName("role")]
        public Role Role { get; set; }

        public Company? Company { get; set; }
        //[JsonPropertyName("companyID")]
        public Guid CompanyId { get; set; }
        public List<OfficeDay> DaysAtOffice { get; set; }
        public User(Guid id, string firstName, string lastName, string password, string email, Role role, Company? company)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Company = company ?? null;
            Role = role;
            DaysAtOffice = new List<OfficeDay>();
        }

        public User(Guid id, string firstName, string lastName, string password, string email, Role role, Guid companyid, Company? company)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Company = company ?? null;
            Role = role;
            CompanyId = companyid;
            DaysAtOffice = new List<OfficeDay>();
        }
        public User(string firstName, string lastName, string password, string email, Role role, Company? company)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Company = company ?? null;
            Role = role;
            DaysAtOffice = new List<OfficeDay>();
        }

        public User(string firstName, string lastName, string password, string email, Role role, Guid companyid,
            List<OfficeDay> officeDays) : this(firstName, lastName, password, email, role, null)
        {
            DaysAtOffice = officeDays;
            CompanyId = companyid;
        }
        public User()
        {

        }
    }
}
