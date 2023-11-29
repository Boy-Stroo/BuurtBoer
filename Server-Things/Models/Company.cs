using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Server_Things.Models
{
    public class Company
    {
        public Guid Id { get; init; }

        private string _name;
        public string Name
        {
            get
            {
                TextInfo textInfo = new CultureInfo("nl-NL", false).TextInfo;
                return textInfo.ToTitleCase(_name);
            }
            set
            {
                _name = value.ToLower();
            }
        }
        public string Description { get; set; }
        public string Address { get; set;}
        public ICollection<User> Users { get; set; }

        public Company(string name, string description, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Address = address;
            Users = new List<User>();
        }

        public Company(string name, string description, string address, ICollection<User> users) : this(name,
            description, address)
        {
            Users = users;
        }

        public Company()
        {
            
        }

        public override string ToString()
        {  
            return $"Name: {Name}\n Description: {Description}\n Address: {Address}";
        }
    }
}
