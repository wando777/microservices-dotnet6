using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model.Context;
using GeekShopping.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure MySQL connection
var configuration = builder.Configuration;
var connectionString = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySqlContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 3, 0)))
);

builder
    .Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MySqlContext>()
    .AddDefaultTokenProviders();

// Configure Identity
IdentityConfiguration.SetConfiguration(configuration);
var createIdentity = builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    }).AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddAspNetIdentity<ApplicationUser>();

createIdentity.AddDeveloperSigningCredential();

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
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
