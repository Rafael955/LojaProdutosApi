using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Dtos.Responses
{
    public class FornecedorResponseDto
    {
        public Guid? Id { get; set; }

        public string? Nome { get; set; }

        public List<ProdutoResponseDto> Produtos { get; set; }

    }
}
