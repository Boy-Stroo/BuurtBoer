using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Web_App;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient("https://localhost:5077");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Commented this to try out HTTP Request underneath

// app.Run();


// UserController has the UserServiceHttpclient
UserController Users = new();
await Users.GetAllUsers();
//printing all users to console
Users.Users.ToList().ForEach(x => Console.WriteLine($"{x.FirstName}, {x.Email}"));
//Checking login
if (await Users.LogIn(new("emp2@microsoft.com", "pass2")))
{
    //Logged in user is saved in the Controller
    User user = Users.CurrentUser!;
    Console.WriteLine($"Success baby: {user.FirstName} {user.LastName}");
}
else
{
    Console.WriteLine("altijd janken");
}