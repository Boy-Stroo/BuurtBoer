namespace Web_App
{
    public class Grocery
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string? Container { get; set; }
        public int Amount { get; set; }
        public Guid? CompanyID { get; set; }
        public Company? Company { get; set; }

        public Grocery(string name, int amount, Guid? companyid)
        {
            Name = name;
            Amount = amount;
            CompanyID = companyid;
            Id = Guid.NewGuid();
        }

        public Grocery() { }

        public string toString => ToString();

        public override string ToString()
        {
            if (Container == null)
            {
                return $"{Amount} {Name}";
            }
            else
            {
                if (Amount == 1)
                {
                    return $"{Amount} {Container} of {Name}";
                }
                else
                {
                    return $"{Amount} {Container}s of {Name}";
                }
            }
        }
    }
}
