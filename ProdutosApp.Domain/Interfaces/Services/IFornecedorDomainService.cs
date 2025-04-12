using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProdutosApp.Domain.Dtos.Requests;
using ProdutosApp.Domain.Dtos.Responses;

namespace ProdutosApp.Domain.Interfaces.Services
{
    public interface IFornecedorDomainService
    {
        FornecedorResponseDto CriarFornecedor(FornecedorRequestDto request);

        FornecedorResponseDto AlterarFornecedor(Guid? id, FornecedorRequestDto request);

        FornecedorResponseDto ExcluirFornecedor(Guid? id);

        FornecedorResponseDto ObterFornecedorPorId(Guid? id);

        List<FornecedorResponseDto> ObterFornecedores();
    }
}
