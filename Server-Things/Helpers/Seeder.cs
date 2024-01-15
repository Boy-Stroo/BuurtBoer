
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
            new Company("Apple", "Hardware", "1234AB, Applestraat 1, Amsterdam"),
            new Company("Facebook", "Social Media", "1234AB, Facebookstraat 1, Amsterdam"),
            new Company("Amazon", "Webshop", "1234AB, Amazonstraat 1, Amsterdam"),
            new Company("Tesla", "Auto's", "1234AB, Teslstraat 1, Amsterdam"),
            new Company("Netflix", "Streaming", "1234AB, Netflixstraat 1, Amsterdam"),
            new Company("Spotify", "Muziek", "1234AB, Spotifystraat 1, Amsterdam"),
            new Company("Bol.com", "Webshop", "1234AB, Bolstraat 1, Amsterdam"),
            new Company("Coolblue", "Webshop", "1234AB, Coolbluestraat 1, Amsterdam"),
            new Company("KPN", "Telecom", "1234AB, KPNstraat 1, Amsterdam"),
            new Company("Odido", "Telecom", "1234AB, Odidostraat 1, Amsterdam"),
            new Company("Vodafone", "Telecom", "1234AB, Vodafonestraat 1, Amsterdam"),
            new Company("Ziggo", "Telecom", "1234AB, Ziggostraat 1, Amsterdam"),
            new Company("KLM", "Vliegtuigen", "1234AB, Schiphol, Amsterdam"),
            new Company("Transavia", "Vliegtuigen", "1234AB, Schiphol, Amsterdam"),
            new Company("TUI", "Vliegtuigen", "1234AB, Schiphol, Amsterdam"),
            new Company("Ryanair", "Vliegtuigen", "1234AB, Schiphol, Amsterdam"),
            new Company("Easyjet", "Vliegtuigen", "1234AB, Schiphol, Amsterdam"),
            new Company("KFC", "Fastfood", "1234AB, KFCstraat 1, Amsterdam"),
            new Company("McDonalds", "Fastfood", "1234AB, McDonaldsstraat 1, Amsterdam"),
            new Company("Burger King", "Fastfood", "1234AB, Burger Kingstraat 1, Amsterdam"),
            new Company("Subway", "Fastfood", "1234AB, Subwaystraat 1, Amsterdam"),
            new Company("Domino's", "Fastfood", "1234AB, Domino'sstraat 1, Amsterdam"),
            new Company("Albert Heijn", "Supermarkt", "1234AB, Albert Heijnstraat 1, Amsterdam"),
            new Company("Jumbo", "Supermarkt", "1234AB, Jumbostraat 1, Amsterdam"),
            new Company("Lidl", "Supermarkt", "1234AB, Lidlstraat 1, Amsterdam"),
            new Company("Aldi", "Supermarkt", "1234AB, Aldistraat 1, Amsterdam"),
            new Company("Plus", "Supermarkt", "1234AB, Plusstraat 1, Amsterdam"),
            new Company("Spar", "Supermarkt", "1234AB, Sparstraat 1, Amsterdam"),
        };

        private static readonly List<User> AdminList = new List<User>()
        {
            new User("Admin", "Van de Admins", Encrypter.Encrypt("admin"), "admin@ad.min", Role.SuperAdmin, CompanyList[0]),
        };

        private static void PopulateAdminList()
        {
            for (int i = 0; i < CompanyList.Count; i++)
            {
                var user = new User($"Admin", $"of {CompanyList[i].Name}",
                    Encrypter.Encrypt($"admin"), $"admin@{CompanyList[i].Name}.com", Role.Admin, CompanyList[i]);
                Db.Users.Add(user);
            }
        }

        private static void PopulateEmployeeList()
        {
            string[] firstNames = new string[] {
                "Jimmy", "Jasper", "Casper", "Jurrian", "Dirk", "Kanye", "Emma", "Mia", "Bas", "Jan", "Frankie", "Francis", "Tommy", "Zlatan", "Mohammed", "Emilia", "Nikita", "Megan", "Anita", "Lisa", "Riley", "Jamie", "Jesper", "Jappie", "Jaap", "Albert", "Heijn", "Adriaan", "Bassie", "Anna", "Kim", "Sophia", "Patricia", "Linda", "Sanne", "Samantha", "Sara", "Suzanne", "Suzanna", "Suzan", "Suzie",
                "Michael", "Peter", "Anton", "Alexander", "Maya", "Adrianus", "Bastiaan", "Benjamin", "Cornelis", "Daan", "Diederik", "Dirk", "Erik", "Floris", "Gerrit", "Hendrik", "Jacobus", "Jan", "Johannes", "Lucas", "Maarten", "Martijn", "Matthijs", "Maurits", "Nick", "Noah", "Pieter", "Richard", "Rob", "Samuel", "Simon", "Steven", "Thomas", "Tim", "Tom", "Victor", "Willem", "William", "Noura",
                "Sophie", "Emma", "Julia", "Anna", "Sarah", "Laura", "Lisa", "Claire", "Hannah", "Sophia", "Chloe", "Ava", "Madison", "Ella", "Charlotte", "Mila", "Lily", "Gianna", "Abigail", "Emily", "Layla", "Amelia", "Isla", "Zoe", "Camila", "Valerie", "Felicity", "Anne", "Nicole", "Michelle"
            };

            string[] lastNames = new string[] {
                "Kardashian", "Jenner", "West", "East", "Nayes", "North", "Ramsey", "Ronaldo", "Khalifa", "Verpaal", "Verstappen", "Van der Valk", "Van de Kaart", "Van 't Padje", "Paulusma", "Vergeer", "Duits", "Van der Veen", "Van der Wal", "Van der Woude", "Van der Weide", "Van der Wijk", "Van der Zee", "Van der Zwaag", "Van der Zwaan",
                "Jones", "Smith", "Williams", "Burger", "Van den Berg", "Van der Meer", "Baker", "Rodriguez", "Van Leeuwen", "Cooper", "Van Dijk", "Wilson", "De Vries", "Moore", "Bos", "Taylor", "Van den Broek", "Anderson", "De Jong", "Thomas", "Visser", "White", "De Boer", "Wright", "De Groot", "De Wit", "De Bruin", "De Witte", "De Graaf", "De Witte", "De Bruin", "De Wit", "De Graaf",
            };

            Random random = new();

            for (int i = 1; i < CompanyList.Count; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    string firstName = firstNames[random.Next(firstNames.Length)];
                    string lastName = lastNames[random.Next(lastNames.Length)];
                    string password = Encrypter.Encrypt($"wachtwoord");
                    var user = new User(firstName, lastName,
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
            PopulateAdminList();
            PopulateEmployeeList();
            Db.SaveChanges();
        }
    }
}
