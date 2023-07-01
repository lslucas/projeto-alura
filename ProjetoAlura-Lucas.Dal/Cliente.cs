using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAlura_Lucas.Dal {
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Foto de perfil")]
        public string ProfilePic { get; set; }
    }
}
