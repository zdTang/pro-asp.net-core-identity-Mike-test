using Microsoft.EntityFrameworkCore;
using IdentityApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ProductDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:AppDataConnection"]);
});

builder.Services.AddDbContext<IdentityDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:IdentityConnection"],
        opts => opts.MigrationsAssembly("IdentityApp")
    );
});

builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddHttpsRedirection(opts =>
{
    opts.HttpsPort = 44350; // override defautl HTTPS PORT 443
});

var app = builder.Build();
// once the server receive a HTTP request, will be redirect to HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
