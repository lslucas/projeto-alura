﻿using Microsoft.AspNetCore.Mvc;
using ProjetoAlura_Lucas.Dal;
using ProjetoAlura_Lucas.Helpers;
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

        [ActionName("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddData(Cliente cliente)
        {
            if (!ModelState.IsValid) {
                return View("Index");
            }

            cliente.ProfilePic = await BlobHelper.UploadFile(cliente.ProfilePicFile);
            _clienteRepository.Add(cliente);

            return Redirect("/Cliente");
        } 

        public IActionResult RemoveData(int id)
        {
            // pega o item atual antes de remover para, principalmente, remover o blob
            var item = _clienteRepository.GetOne(id);
            if (item != null)
            {
                if (item.ProfilePic != null)
                {
                    // apaga blob antes de apagar o item
                    BlobHelper.DeleteBlob(item.ProfilePic);
                }

                _clienteRepository.Remove(id);
            }

            return Redirect("/Cliente");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}