using Microsoft.AspNetCore.Mvc;
using ProjetoAlura_Lucas.Models;
using System.Diagnostics;

namespace ProjetoAlura_Lucas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Página Inicial";
            ViewData["SiteName"] = "Projeto Alura: Lucas Serafim";

            return View();
        }

        public IActionResult Whoami()
        {
            ViewData["Title"] = "Quem sou eu?";
            ViewData["SiteName"] = "Projeto Alura: Lucas Serafim";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}