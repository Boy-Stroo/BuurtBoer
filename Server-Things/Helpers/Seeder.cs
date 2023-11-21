
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
            new User("Admin", "Van de Admins", "abcd", "admin@deadmins.com", Role.SuperAdmin, CompanyList[0]),
            new User("Admin", "van den Google", "1234", "adminvdgoogle@google.com", Role.Admin, CompanyList[1]),
            new User("Admin", "van den Microsoft", "wachtwoord", "admin@dereelmicrosoft.com", Role.Admin, CompanyList[2]),
            new User("Admin", "'s Apple", "qwerty78", "admin@TruApple.com", Role.Admin, CompanyList[3])
        };

        private static void PopulateEmployeeList()
        {

            var user = new User($"Employee1", $"Employee of {CompanyList[1].Name}",
                $"pass", $"emp1@somewhere.com", Role.Employee, CompanyList[1]);
            Db.Users.Add(user);
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
