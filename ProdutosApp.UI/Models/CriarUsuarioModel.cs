using System.ComponentModel.DataAnnotations;

namespace ProdutosApp.UI.Models
{
    public class CriarUsuarioModel
    {
        [MinLength(8, ErrorMessage = "Por favor, informe o nome com no mínimo {1} caracteres")]
        [Required(ErrorMessage = "Por favor, informe o seu nome.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o seu endereço de acesso.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe a sua senha de acesso.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha de acesso.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
