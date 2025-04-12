using ProdutosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Dtos.Responses
{
    public class ProdutoResponseDto
    {
        public Guid? Id { get; set; }

        public string? Nome { get; set; }

        public decimal? Preco { get; set; }

        public int? Quantidade { get; set; }

        public decimal? Total => Preco * Quantidade;

        public Guid? FornecedorId { get; set; }

        public string? NomeFornecedor { get; set; }
    }
}
