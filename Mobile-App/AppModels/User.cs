using System.Runtime.Serialization;

namespace Mobile_App
{
    public enum Role
    {
        SuperAdmin,
        Admin,
        Employee
    }
    //To direct the JSON serializer to serialize this class
    [DataContract]
    public class User
    {
        private string _email;
        //Has to do with mapping serialization of JSON to C# Property
        [DataMember]
        public Guid Id { get; init; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email
        {
            get => _email;
            set => _email = value.ToLower();
        }
        [DataMember]
        public Role Role { get; set; }

        [DataMember]
        public Company Company { get; set; }

        [DataMember]
        public Guid? CompanyId { get; set; }
        [DataMember]
        public List<OfficeDay> DaysAtOffice { get; set; }
        public string ListViewString => $"{FirstName} {LastName} - {Email}";
        public User(Guid id, string firstName, string lastName, string password, string email, Role role, Company company)
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
        public User(string firstName, string lastName, string password, string email, Role role, Company company)
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

        public User(string firstName, string lastName, string password, string email, Role role, Guid? companyid,
            List<OfficeDay> officeDays) : this(firstName, lastName, password, email, role, null)
        {
            DaysAtOffice = officeDays;
            CompanyId = companyid;
        }
        // Default constructor needed for deserialization
        public User()
        {
            DaysAtOffice = new List<OfficeDay>();
        }
    }
}
