using Microsoft.AspNetCore.Mvc;
using ProjetoAlura_Lucas.Dal;
using ProjetoAlura_Lucas.Models;
using System.Diagnostics;

namespace ProjetoAlura_Lucas.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        public readonly IClienteRepository _clienteRepository;
        public Cliente Cliente { get; set; }

        public ClienteController(ILogger<ClienteController> logger, IClienteRepository clienteRepository)
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Clientes";
            ViewData["SiteName"] = "Projeto Alura: Lucas Serafim";

            return View();
        }

        public IActionResult OnPost(Cliente cliente)
        {
            if (!ModelState.IsValid) {
                return View("Index");
            }

            _clienteRepository.Add(cliente);

            return RedirectToPage("/Clientes");
        } 


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}