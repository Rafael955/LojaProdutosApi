using ProdutosApp.Domain.Dtos.Responses;
using ProdutosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Interfaces.Repositories
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Fornecedor? GetByName(string nome);
    }
}
