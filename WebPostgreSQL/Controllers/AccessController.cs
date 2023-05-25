using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebPostgreSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace WebPostgreSQL.Controllers
{
    public class AccessController : Controller
    {

        private readonly Contexto _context;

        public AccessController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            //var testecontext = _context.usuarios.ToListAsync();
            string sSql = @"SELECT * FROM usuario";
            IQueryable<Usuario> testeFromSqlRaw = _context.usuarios.FromSqlRaw(sSql);  // FromSqlRaw é sujeito a sql Injection, usar somente quando não recebe interpolação de fora

            // todo: Implementar validação de usuários
            if (modelLogin.Email == "admin@gmail.com" && modelLogin.PassWord == "admin")
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties","Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
    }
}