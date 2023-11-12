
using Microsoft.EntityFrameworkCore;

namespace Server_Things
{
    public class BuurtboerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Port=12345;Database=buurtboer;Username=admin;Password=44dPA7k");
            builder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            Console.WriteLine($"Builder is configured: {builder.IsConfigured}");
        }
    }
}
