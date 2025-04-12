using ProdutosApp.Domain.Dtos.Requests;
using ProdutosApp.Domain.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Interfaces.Services
{
    public interface IProdutoDomainService
    {
        ProdutoResponseDto CriarProduto(ProdutoRequestDto request);

        ProdutoResponseDto AlterarProduto(Guid? id, ProdutoRequestDto request);

        ProdutoResponseDto ExcluirProduto(Guid? id);

        ProdutoResponseDto ObterProdutoPorId(Guid? id);

        List<ProdutoResponseDto> ObterProdutos();

    }
}
