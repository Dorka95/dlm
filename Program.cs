using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using logindlm.Model;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

// Configure authentication with a login path
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login"; // Bejelentkezési oldal elérési útja
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Redirect root path to login
app.MapGet("/", () => Results.Redirect("/termek"));

// Map Razor Pages
app.MapRazorPages();

app.Run();
