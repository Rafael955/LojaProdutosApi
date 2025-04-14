using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Message.Models
{
    public class ProdutoCriado
    {
        public Guid? Id { get; set; }

        public string? Nome { get; set; }

        public string? Fornecedor { get; set;  }

        public decimal? Preco { get; set; }

        public int? Quantidade { get; set; }

        public string? Usuario { get; set; }

        public string? Email { get; set; }

        public DateTime? CriadoEm { get; set; }
    }
}
