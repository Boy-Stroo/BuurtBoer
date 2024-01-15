
using Server_Things.Models;
using Server_Things.Factories;


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
                "Jimmy", "Jasper", "Jamie", "Jesper", "Jappie", "Jaap", "Albert", "Heijn", "Adriaan", "Bassie", "Anna", "Kim", "Sophia", "Patricia"
            };

            string[] lastNames = new string[] {
                "Kardashian", "Ramsey", "Ronaldo", "Verpaal", "Verstappen", "Van der Valk", "Van de Kaart", "Van 't Padje", "Paulusma", "Vergeer", "Duits"
            };

            Random random = new();

            for (int i = 1; i < CompanyList.Count; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    string firstName = firstNames[random.Next(firstNames.Length)];
                    string lastName = lastNames[random.Next(lastNames.Length)];
                    string password = Encrypter.Encrypt($"pass{j}");
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

        public static void Clear()
        {
            //Db.OfficeDays.RemoveRange(Db.OfficeDays);
            Db.Users.RemoveRange(Db.Users);
            Db.Companies.RemoveRange(Db.Companies);
            //Db.SaveChanges();
        }

        public static void Seed()
        {
            Clear();
            Db.Companies.AddRange(CompanyList);
            Db.Users.AddRange(AdminList);
            PopulateAdminList();
            PopulateEmployeeList();
            Db.SaveChanges();
        }
    }
}
