using Microsoft.EntityFrameworkCore;
using Npgsql;
using Server_Things;
using Server_Things.Models;
using Server_Things.Factories;
public class DatabaseFixture : IDisposable
{
    public BuurtboerContext Db;

    private Company _company;
    private List<User> _users;
    private List<OfficeDay> _officeDays;


    public DatabaseFixture()
    {

        Db = DbContextFactory.Create("Host=localhost;Port=5432;Database=buurtboertest;Username=postgres;Include Error Detail=true");
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