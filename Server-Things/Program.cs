using Renci.SshNet;
using Server_Things;


var client = new SshClient("145.24.222.23", 22, "ubuntu-1059195", "44dPA7k");
client.Connect();

var forwardedPort = new ForwardedPortLocal("localhost", 12345, "145.24.222.23", 5432);
client.AddForwardedPort(forwardedPort);
forwardedPort.Start();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var context = new BuurtboerContext())
{
    Console.WriteLine($"Context is configured: {context.Database.CanConnect()}");
    app.Run();
}
