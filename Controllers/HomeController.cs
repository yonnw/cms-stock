﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cms_stock.Models;
using Microsoft.AspNetCore.Http;
using cms_stock.Models.Infraestrutura.Autenticacao;

namespace cms_stock.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Logado]

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {       
            return View();
        }

        public IActionResult Logout()
        {
            this.HttpContext.Response.Cookies.Delete("adm_cms_dv");
            return Redirect("/login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
