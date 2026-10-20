using Microsoft.EntityFrameworkCore;
using CreditWorks;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CreditWorksContext") ?? throw new InvalidOperationException("Connection string 'CreditWorksContext' not found.");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vehicle}/{action=Index}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "categoryDefault",
    pattern: "{controller=WeightCatergory}/{action=Index}")
    .WithStaticAssets();

app.Run();
