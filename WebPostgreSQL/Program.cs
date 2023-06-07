using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebPostgreSQL.Models;

var sqlConnectionStringBuilder = WebApplication.CreateBuilder(args);

// Add serviço no container.
sqlConnectionStringBuilder.Services.AddControllersWithViews();

//Add a parte de Login
sqlConnectionStringBuilder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Access/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});

const string _stringDeConnexao = "Host=localhost;Port=5432;Pooling=true;Database=SISTEMALEITE;User Id=postgres;Password=admin;";

sqlConnectionStringBuilder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<Contexto>(option => option.UseNpgsql(_stringDeConnexao));
//.AddDbContext<Contexto>(option => option.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=CRUD_POSTGRE;User Id=postgres;Password=lucasdewes10;"));


var app = sqlConnectionStringBuilder.Build();

// COnfigura the HTTP request pipeline.
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
