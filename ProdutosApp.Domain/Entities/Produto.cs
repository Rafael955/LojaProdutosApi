using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Entities
{
    public class Produto
    {
        public Guid? Id { get; set; }

        public string? Nome { get; set; }

        public decimal? Preco { get; set; }

        public int? Quantidade { get; set; }

        #region Relacionamentos

        public Guid? FornecedorId { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        
        #endregion
    }
}
