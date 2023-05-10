using Microsoft.EntityFrameworkCore;
using WebPostgreSQL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add a parte de Login
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Access/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});


builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<Contexto>(option => option.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=CRUD_POSTGRE;User Id=postgres;Password=lucasdewes10;"));


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}"); //alterado para Login
    //pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
