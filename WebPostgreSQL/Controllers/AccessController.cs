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
                //Chama o método que faz a consulta de usuário
                List<Dictionary<string, object>> testeFromSqlRaw = await Consultas.GetConsultaLoginAsync(modelLogin.PassWord, modelLogin.Email);  // FromSqlRaw é sujeito a sql Injection, usar somente quando não recebe interpolação de fora
                                                                                                                                                  //Caso tenho resultado
                //Caso tenha resultado, faz a validação com o objeto modelLogin
                if (testeFromSqlRaw.Count() > 0)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                        new Claim("OtherProperties","Example Role")
                    };

                    //Configura os cookies para manter a sessão
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = modelLogin.KeepLoggedIn
                    };

                    //Realiza o login para o usuário
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Home");
                }

                //Se não foi redirecionado acima, exibe msg de erro
                ViewData["ValidateMessage"] = "Usuário não encontrado ou senha incorreta";
                return View();
            }
            catch (Exception ex)
            {
                string sMensagemDeErro = string.Empty;

                if (ex.Message == "Couldn't set port (Parameter 'port')")
                    sMensagemDeErro = "Não foi possível se conectar com o banco: Erro no endereço ou na porta";
                else if (ex.Message.Substring(0, 5) == "3D000")
                    sMensagemDeErro = "Não foi possível se conectar com o banco: A base informada não existe";
                else if (ex.Message.Substring(0, 5) == "28P01")
                    sMensagemDeErro = "Não foi possível se conectar com o banco: Login ou senha do admin incorretos na string de conexão";
                else
                    sMensagemDeErro ="AccessController - Login: " + ex.Message;

                ViewData["ValidateMessage"] = sMensagemDeErro;
                return View();
            }
        }
    }
}