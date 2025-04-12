using Microsoft.EntityFrameworkCore;
using ProdutosApp.Domain.Dtos.Responses;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        public void Add(Produto produto)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(produto);
                dataContext.SaveChanges();
            }
        }

        public void Update(Produto produto)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(produto);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Produto produto)
        {
            using(var dataContext = new DataContext())
            {
                dataContext.Remove(produto);
                dataContext.SaveChanges();
            }
        }

        public Produto? GetById(Guid? id)
        {
            using (var dataContext = new DataContext())
            {
                //LAMBDA
                return dataContext.Set<Produto>()
                    .Include(x => x.Fornecedor)
                    .SingleOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Produto> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                //LAMBDA
                return dataContext.Set<Produto>()
                    .Include(x => x.Fornecedor)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Produto? GetByName(string nome)
        {
            using (var dataContext = new DataContext())
            {
                //LAMBDA
                return dataContext.Set<Produto>()
                    .Include(x => x.Fornecedor)
                    .SingleOrDefault(x => x.Nome == nome);
            }
        }

        /// <summary>
        /// Método para consultar o somatório da quantidade de produtos
        /// para cada fornecedor do banco de dados
        /// </summary>
        /// <returns>Uma lista com os fornecedores e o somatório de seus produtos</returns>
        public List<FornecedorProdutosResponseDto> GroupByFornecedor()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Produto>() //Tabela de produtos
                    .Include(p => p.Fornecedor) //Junção com tabela de categorias
                    .GroupBy(p => p.Fornecedor.Nome) //Agrupando pelo nome da categoria
                    .Select(g => new FornecedorProdutosResponseDto
                    {
                        Fornecedor = g.Key, //Nome da categoria
                        Produtos = g.Sum(p => p.Quantidade) //Somatório da quantidade de produtos
                    })
                    .ToList(); //Retornar uma lista do DTO
            }
        }
    }
}
