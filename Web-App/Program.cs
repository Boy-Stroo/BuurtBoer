using System.Text.Json;
using System.Text.Json.Serialization;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Web_App.Data;

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

// Comment this to try out HTTP Request underneath
app.Run();


// Create HTTP client
// Register the service
// Await request


// HttpClient http = new();
// http.BaseAddress = new Uri("http://localhost:5077");
// EmployeeService employeeService = new(http);
// var result = await employeeService.GetEmployees();
// Console.WriteLine("Hi");
// result.ToList().ForEach(x => Console.WriteLine(x));