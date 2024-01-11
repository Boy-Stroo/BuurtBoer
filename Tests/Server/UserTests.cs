namespace Server;
using Server_Things;
using Server_Things.Controllers;
using Server_Things.Models;

public class UserTests : IClassFixture<DatabaseFixture>
{
    DatabaseFixture fixture;
    UserController UserController = new();

    public UserTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
    }
    
    [Fact]
    public void Login()
    {
        // Arrange
        User employee = fixture.Db.Users.Where(u => u.Role == Role.Employee).First();
        var credentials = (employee.Email, employee.Password);

        // Act
        var result = UserController.GetLoggedInUser(new(employee.Email, employee.Password));
    }
}