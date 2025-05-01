namespace ProdutosApp.UI.Models
{
    public class UsuarioLogadoModel
    {
        public Guid? Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? Perfil { get; set; }

        public DateTime? AcessoEm { get; set; }

        public string? Token { get; set; }

        public DateTime? Expiracao { get; set; }
    }
}
