namespace Server;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Server_Things;
using Server_Things.Controllers;
using Server_Things.Models;
using Server_Things.Factories;

public class UserTests : IClassFixture<DatabaseFixture>
{
    DatabaseFixture fixture;
    readonly CompanyController CompanyController = new();
    readonly UserController UserController = new();



    public UserTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
        //CompanyController = new CompanyController(fixture.Db);
    }
    
    [Fact]
    public async void GetAllUsers()
    {
        // Arrange
        // Act
        var result = await UserController.GetUsers();

        // Assert
        // var users = JsonSerializer.Deserialize<List<User>>(result.Value.ToString());
        // foreach (var user in fixture.Db.Users)
        // {
        //     Assert.Contains(user, result.Value);
        // }
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

    [Fact]
    public async void AddUser()
    {
        // Arrange
        User user = new("firstName", "lastName", "password", "email@gmail.com", Role.Employee, null);

        // Act
        await UserController.AddUser(user);

        // Assert
        var addedUser = fixture.Db.Users.FirstOrDefault(u => u.Email == user.Email);
        Assert.NotNull(addedUser);
        await UserController.DeleteUsersDatabase(addedUser.Id);
    }

    [Fact]
    public async void DeleteUsersDatabase()
    {
        User usertodelete = new("firstName2", "lastName2", "password2", "email2@gmail.com", Role.Employee, null);

        await UserController.AddUser(usertodelete);
        await UserController.DeleteUsersDatabase(usertodelete.Id);
        var deletedUser = fixture.Db.Users.FirstOrDefault(u => u.Id == usertodelete.Id);

        Assert.Null(deletedUser);
    }

    [Fact]
    public async void AddCompany()
    {
        // Arrange
        Company company = new Company("companynameR", "Everything for you", "1234AB, abcstraat 1, Rotterdam");

        // Act
        await CompanyController.AddCompany(company);
        // Assert
        var addedCompany = fixture.Db.Companies.FirstOrDefault(c => c.Description == company.Description);
        Assert.NotNull(addedCompany);
        await CompanyController.DeleteCompaniesDatabase(addedCompany.Id);
    }

    [Fact]
    public async void DeleteCompaniesDatabase()
    {
        Company company = new Company("companyname2", "Everything for you2", "1234AB, abcstraat 2, Rotterdam");

        await CompanyController.AddCompany(company);
        await CompanyController.DeleteCompaniesDatabase(company.Id);
        var deletedCompany = fixture.Db.Companies.FirstOrDefault(c => c.Id == company.Id);

        Assert.Null(deletedCompany);
    }  
}