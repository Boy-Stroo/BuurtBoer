
namespace Server_Things.Helpers
{
    public static class Seeder
    {
        private static readonly BuurtboerContext Db = new BuurtboerContext();

        public static void Clear()
        {
            Db.OfficeDays.RemoveRange(Db.OfficeDays);
            Db.Users.RemoveRange(Db.Users);
            Db.Companies.RemoveRange(Db.Companies);
        }
    }
}
