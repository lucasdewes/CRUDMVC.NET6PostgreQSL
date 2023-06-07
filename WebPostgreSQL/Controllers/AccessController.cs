using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebPostgreSQL.Models;

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
            try
            {
                List<Dictionary<string, object>> testeFromSqlRaw = await Consultas.GetConsultaLoginAsync(modelLogin.PassWord, modelLogin.Email);  // FromSqlRaw é sujeito a sql Injection, usar somente quando não recebe interpolação de fora
                if (testeFromSqlRaw.Count() > 0)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                        new Claim("OtherProperties","Example Role")
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = modelLogin.KeepLoggedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Home");
                }

                ViewData["ValidateMessage"] = "Usuário não encontrado ou senha incorreta";
                return View();
            }
            catch (Exception ex)
            {
                ViewData["ValidateMessage"] = "Erro no Login(): " + ex.Message;
                return View();
            }
        }
    }
}