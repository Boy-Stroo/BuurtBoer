
using Server_Things.Models;

namespace Server_Things.Helpers
{
    public static class Seeder
    {
        private static readonly BuurtboerContext Db = new BuurtboerContext();

        private static readonly List<Company> CompanyList = new List<Company>()
        {
            new Company("De Buurtboer", "Verzorgd lunches bij bedrijven", "1234AB, Kerkstraat 1, Groningen"),
            new Company("Google", "Zoekmachine", "4321BA, Googlestraat 1, Amsterdam"),
            new Company("Microsoft", "Software", "1234AB, Microsoftstraat 1, Amsterdam"),
            new Company("Apple", "Hardware", "1234AB, Applestraat 1, Amsterdam")
        };

        private static readonly List<User> AdminList = new List<User>()
        {
            new User("Admin", "Van de Admins", "admin", "admin@ad.min", Role.SuperAdmin, CompanyList[0]),
            new User("Admin", "van den Google", "wachtwoord", "admin@google.com", Role.Admin, CompanyList[1]),
            new User("Admin", "van der Microsoft", "wachtwoord", "admin@microsoft.com", Role.Admin, CompanyList[2]),
            new User("Admin", "'s Apple", "wachtwoord", "admin@apple.com", Role.Admin, CompanyList[3])
        };

        private static void PopulateEmployeeList()
        {
            for (int i = 1; i < CompanyList.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var user = new User($"Employee{j}", $"of {CompanyList[i].Name}",
                        $"pass{j}", $"emp{j}@{CompanyList[i].Name}.com", Role.Employee, CompanyList[i]);
                    Db.Users.Add(user);

                    Random rand = new();
                    if (rand.Next(0, 8) != 7 && user.Role == Role.Employee)
                    {
                        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                        for (int x = 0; x < 7; x++)
                        {
                            if (rand.Next(2) == 1)
                            {
                                Db.OfficeDays.Add(new(user.Id, today));
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
            PopulateEmployeeList();
            Db.SaveChanges();
        }
    }
}
