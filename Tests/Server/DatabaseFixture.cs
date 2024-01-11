using Microsoft.EntityFrameworkCore;
using Npgsql;
using Server_Things;
using Server_Things.Models;

public class DatabaseFixture : IDisposable
{
    public TestDbContext Db;

    private Company _company;
    private List<User> _users;
    private List<OfficeDay> _officeDays;


    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=buurtboer;Username=postgres;Include Error Detail=true");  

        Db = new TestDbContext(options.Options);
        _company = new("test", "test", "test");
        
        User employee = new("Foo", "Bar", "FooBar", "emp@foo.bar", Role.Employee, _company);
        User admin = new("Foo", "Bar", "FooBar", "admin@foo.bar", Role.Admin, _company);
        User superadmin = new("Foo", "Bar", "FooBar", "supadmin@foo.bar", Role.SuperAdmin, _company);

        _users = new List<User> {employee, admin, superadmin};
        _officeDays = new();

        Db.Companies.Add(_company);
        Db.Users.AddRange(_users);
        Db.SaveChanges();
    }


    public void Dispose()
    {
        Db.RemoveRange(_users);
        Db.Remove(_company);
        Db.SaveChanges();
    }
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