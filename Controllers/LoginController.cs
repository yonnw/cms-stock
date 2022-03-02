using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cms_stock.Models;
using Microsoft.AspNetCore.Http;
using cms_stock.Models.Infraestrutura.Database;

namespace cms_stock.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public LoginController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("login/signin")]
        [HttpPost]

        public IActionResult Signin(string email, string password)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.erro = "Coloque o email e password";
            }
            var adms = new ContextoCms().Administradores.Where(a => a.Email == email && a.Password == password).ToList();
            if (adms.Count > 0)
            {
                if (adms.First().Admin == true)
                {
                    this.HttpContext.Response.Cookies.Append("adm_cms_dv", adms.First().Id.ToString(), new CookieOptions()
                    {
                        Expires = DateTimeOffset.UtcNow.AddMonths(6),
                        HttpOnly = true
                    });

                    Response.Redirect("/");
                }
                else
                {
                    this.HttpContext.Response.Cookies.Append("user_cms_dv", adms.First().Id.ToString(), new CookieOptions()
                    {
                        Expires = DateTimeOffset.UtcNow.AddMonths(6),
                        HttpOnly = true
                    });

                    Response.Redirect("/centrocustos/indexuser");
                }
            }
            else
            {
                ViewBag.erro = "Email ou password inválidos";
            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
