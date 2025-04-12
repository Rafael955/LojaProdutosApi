using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Dtos.Responses
{
    public class FornecedorProdutosResponseDto
    {
        public string? Fornecedor { get; set;  }

        public int? Produtos { get; set; }
    }
}
