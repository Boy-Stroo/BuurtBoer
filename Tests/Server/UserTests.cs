namespace Server;

using Microsoft.AspNetCore.Mvc;
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
    public async void Login()
    {
        // Arrange
        User employee = fixture.Db.Users.Where(u => u.Role == Role.Employee).First();
        UserCredentials credentials = new(employee.Email, employee.Password);

        // Act
        var result = await UserController.GetLoggedInUser(credentials);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void BadLogin()
    {
        // Arrange
        UserCredentials credentials = new("foobar@foo.bar", "foobar");

        // Act
        var result = await UserController.GetLoggedInUser(credentials);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}