using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server_Things.Models
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
        [Key]
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public string Email
        {
            get => _email;
            set => _email = value.ToLower();
        }
        public Role Role { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
        
        public Guid? CompanyId { get; set; }
        public List<DaysAtOffice> DaysAtOffice { get; set; }

        public User(string firstName, string lastName, string password, string email, Role role, Company? company)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Company = company ?? null;
            Role = role;
            DaysAtOffice = new List<DaysAtOffice>();
        }

        public User(string firstName, string lastName, string password, string email, Role role, Company? company,
            List<DaysAtOffice> officeDays) : this(firstName, lastName, password, email, role,company)
        {
            DaysAtOffice = officeDays;
        }

        public User()
        {

        }
    }
}
