using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Server_Things;

namespace Server_Things.Factories
{ 
    public class DbContextFactory : IDesignTimeDbContextFactory<BuurtboerContext>
    {
        public static BuurtboerContext Create() => Create("Host=localhost;Port=5432;Database=buurtboer;Username=postgres;Include Error Detail=true");   

        public static BuurtboerContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BuurtboerContext>();
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Error);
            return new BuurtboerContext(optionsBuilder.Options);
        }

        public BuurtboerContext CreateDbContext(string[] args)
        {
            return Create();
        }
    }
}