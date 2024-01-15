
using Server_Things.Models;
using Server_Things.Factories;
using System.Collections.Generic;
using System.Xml.Linq;


namespace Server_Things.Helpers
{
    public static class Seeder
    {
        private static readonly BuurtboerContext Db = DbContextFactory.Create("Host=localhost;Port=5432;Database=buurtboer;Username=postgres;Include Error Detail=true");

        private static readonly List<Company> CompanyList = new List<Company>()
        {
            new Company("De Buurtboer", "Verzorgd lunches bij bedrijven", "1234AB, Kerkstraat 1, Groningen"),
            new Company("Google", "Zoekmachine", "4321BA, Googlestraat 1, Amsterdam"),
            new Company("Microsoft", "Software", "1234AB, Microsoftstraat 1, Amsterdam"),
            new Company("Apple", "Hardware", "1234AB, Applestraat 1, Amsterdam")
        };

        private static readonly List<User> AdminList = new List<User>()
        {
            new User("Admin", "Van de Admins", Encrypter.Encrypt("admin"), "admin@ad.min", Role.SuperAdmin, CompanyList[0]),
            new User("Admin", "van den Google", Encrypter.Encrypt("wachtwoord"), "admin@google.com", Role.Admin, CompanyList[1]),
            new User("Admin", "van der Microsoft", Encrypter.Encrypt("wachtwoord"), "admin@microsoft.com", Role.Admin, CompanyList[2]),
            new User("Admin", "'s Apple", Encrypter.Encrypt("wachtwoord"), "admin@apple.com", Role.Admin, CompanyList[3])
        };

        private static void PopulateEmployeeList()
        {
            for (int i = 1; i < CompanyList.Count; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    string password = Encrypter.Encrypt($"pass{j}");
                    var user = new User($"Employee{j}", $"of {CompanyList[i].Name}",
                        password , $"emp{j}@{CompanyList[i].Name}.com", Role.Employee, CompanyList[i]);
                    Db.Users.Add(user);

                    Random rand = new();
                    if (rand.Next(0, 8) != 7 && user.Role == Role.Employee)
                    {
                        DateOnly today = DateOnly.FromDateTime(DateTime.Today.AddDays(7)); //voor donderdag deze gebruiken bij testing.
                        //DateOnly today = DateOnly.FromDateTime(DateTime.Today); //na woensdag deze gebruiken bij testing.
                        DateOnly lastMonth = DateOnly.FromDateTime(DateTime.Today.AddMonths(-1));
                        for (int x = 0; x < 7; x++)
                        {
                            if (rand.Next(2) == 1)
                            {
                                Db.OfficeDays.Add(new(user.Id, today));
                                Db.OfficeDays.Add(new(user.Id, lastMonth));
                            }
                            today = today.AddDays(1);
                        }
                    }
                }
            }
        }

        private static void PopulateGroceryList()
        {
            for (int i = 1; i < CompanyList.Count; i++)
            {
                List<Grocery> products = new List<Grocery>
                {
                    new Grocery ("loaf(s) of bread", 6, CompanyList[i].Id),
                    new Grocery ("package(s) of cheese slices", 3, CompanyList[i].Id ),
                    new Grocery ("jar(s) of peanut butter", 1, CompanyList[i].Id ),
                    new Grocery ("jar(s) of hagelsag", 1, CompanyList[i].Id ),
                    new Grocery ("jar(s) of nutella", 1, CompanyList[i].Id ),
                    new Grocery ("jar(s) of speculoos", 1, CompanyList[i].Id ),
                    new Grocery ("liter(s) of milk", 2, CompanyList[i].Id ),
                    new Grocery ("package(s) of ham", 2, CompanyList[i].Id ),
                    new Grocery ("package(s) of salami", 2, CompanyList[i].Id ),
                    new Grocery ("liter(s) of juice", 2, CompanyList[i].Id ),
                    new Grocery ("package(s) of butter", 2, CompanyList[i].Id ),
                    new Grocery ("banana(s)", 4, CompanyList[i].Id ),
                    new Grocery ("apple(s)", 3, CompanyList[i].Id )
                };
                Db.AddRange(products);
            }
        }

        public static void Clear()
        {
            //Db.OfficeDays.RemoveRange(Db.OfficeDays);
            Db.Users.RemoveRange(Db.Users);
            Db.Companies.RemoveRange(Db.Companies);
            Db.GroceryList.RemoveRange(Db.GroceryList);
            //Db.SaveChanges();
        }

        public static void Seed()
        {
            Clear();
            Db.Companies.AddRange(CompanyList);
            Db.Users.AddRange(AdminList);
            PopulateGroceryList();
            PopulateEmployeeList();
            Db.SaveChanges();
        }
    }
}
