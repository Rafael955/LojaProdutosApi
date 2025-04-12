using ProdutosApp.Domain.Dtos.Responses;
using ProdutosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Produto? GetByName(string nome);

        List<FornecedorProdutosResponseDto> GroupByFornecedor();
    }
}
