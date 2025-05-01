using System.ComponentModel.DataAnnotations;

namespace ProdutosApp.UI.Models
{
    public class AutenticarUsuarioModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um e-mail válido")]
        [Required(ErrorMessage = "Por favor, informe o seu e-mail de acesso.")]
        public string? Email { get; set; }

        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres")]
        [Required(ErrorMessage = "Por favor, informe a sua senha de acesso.")]
        public string? Senha { get; set; }
    }
}
