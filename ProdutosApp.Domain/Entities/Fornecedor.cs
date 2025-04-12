using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Entities
{
    public class Fornecedor
    {
        public Guid? Id { get; set; }

        public string? Nome { get; set; }

        #region Relacionamentos

        public virtual List<Produto> Produtos { get; set; }

        #endregion
    }
}
