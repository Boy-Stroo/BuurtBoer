namespace Server;
using Server_Things;
using Server_Things.Controllers;
using Server_Things.Models;

public class UserTests : IClassFixture<DatabaseFixture>
{
    DatabaseFixture fixture;

    public UserTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
    }
    
    [Fact]
    public void Login()
    {
        //Arrange
        User user = new(Guid.Empty, "Foo", "Bar", "FooBar", "Foo@b.ar", Role.Employee, null);
        
    }
}