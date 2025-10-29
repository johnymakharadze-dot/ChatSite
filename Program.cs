using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ChatSite.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC მხარდაჭერა
builder.Services.AddControllersWithViews();

// HttpClient — FinancialModelingPrep API-სთვის
builder.Services.AddHttpClient("fmp", client =>
{
    client.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// ჩვენი FmpService რეგისტრაცია Dependency Injection-ში
builder.Services.AddScoped<FmpService>();

var app = builder.Build();

// Routing
app.UseRouting();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// აპის გაშვება
app.Run();
