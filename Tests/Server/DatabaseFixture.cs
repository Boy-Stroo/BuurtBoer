using Microsoft.EntityFrameworkCore;
using Npgsql;
using Server_Things;
using Server_Things.Models;

public class DatabaseFixture : IDisposable
{
    private TestDbContext Db;

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=buurtboer;Username=postgres;Include Error Detail=true");  

        Db = new TestDbContext(options.Options);

        Db.Users.Add(new());
    }

    public void Dispose()
    {
        // ... clean up test data from the database ...
    }

    // public SqlConnection Db { get; private set; }
}
public class TestDbContext : DbContext
{
    public DbSet<User> Users {get; set;}
    public DbSet<Company> Companies{ get; set; }
    public DbSet<OfficeDay> OfficeDays{ get; set; }

    public TestDbContext(DbContextOptions options)
        : base(options)
    {
    }
}