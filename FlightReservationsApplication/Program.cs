using FlightReservationsApplication.Attributes;
using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IAircraftRepository, AircraftRepository>();
builder.Services.AddScoped<IAirlineRepository, AirlineRepository>();
builder.Services.AddTransient<IAirportRepository, AirportRepository>();
builder.Services.AddTransient<IContactInformationRepository, ContactInformationRepository>();
builder.Services.AddTransient<ICreditCardRepository, CreditCardRepository>();
builder.Services.AddTransient<IClassRepository, ClassRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IFlightRepository, FlightRepository>();
builder.Services.AddTransient<ISeatRepository, SeatRepository>();
builder.Services.AddTransient<ISalaryHistoryRepository, SalaryHistoryRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<IReservationConfirmationRepository, ReservationConfirmationRepository>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithRedirects("/Errors/{0}");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
