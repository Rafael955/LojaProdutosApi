using ProdutosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Dtos.Requests
{
    public class ProdutoRequestDto
    {
        public string? Nome { get; set; }

        public decimal? Preco { get; set; }

        public int? Quantidade { get; set; }

        public Guid? FornecedorId { get; set; }
    }
}
