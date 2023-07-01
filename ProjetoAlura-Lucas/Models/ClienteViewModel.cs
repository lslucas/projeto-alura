using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoAlura_Lucas.Dal;

namespace ProjetoAlura_Lucas.Models
{
    public class ClienteViewModel : PageModel
    {
        public readonly IClienteRepository _clienteRepository;

        public Cliente Cliente { get; set; }

        public ClienteViewModel(ClienteRepository clienteRepository)
        {
            this._clienteRepository = clienteRepository;
            this.Cliente = new Cliente();
        }

        public void OnGet() {
            Console.WriteLine("Hello");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _clienteRepository.Add(Cliente);

            return RedirectToPage("/Clientes");
        } 
    }
}