using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;

namespace Server_Things.Models
{
    public class Grocery
    {
        public Guid Id { get; init; }
        [Required]
        public string Name { get; set; }
        public string? Container { get; set; }
        public int Amount { get; set; }
        public Guid? CompanyID { get; set; }
        public Company? Company { get; set; }

        public Grocery(string name, string container, int amount, Guid? companyid) 
        {
            Name = name;
            Container = container;
            Amount = amount;
            CompanyID = companyid;
            Id = Guid.NewGuid();
        }

        public Grocery() { }
    }
}
