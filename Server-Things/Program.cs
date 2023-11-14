using Server_Things;


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
    //var Buurtboer = new Company("De Buurtboer", "De Buurtboer is een bedrijf dat lunchpakketten levert aan bedrijven.",
    //    "Kruislaan 419, 1098 SJ Amsterdam");

    //var user = new User("Admin", "van der admin", "admin", "admin@ad.min", Role.SuperAdmin, Buurtboer);

    //context.Companies.Add(Buurtboer);
    //context.Users.Add(user);
    //context.SaveChanges();

    //var query = from c in context.Companies
    //                            select new Company(c.Name, c.Description, c.Address);

    //foreach (var company in query)
    //{
    //    Console.WriteLine();
    //    Console.WriteLine(company.ToString());
    //    Console.WriteLine();
    //}

    app.Run();

}
