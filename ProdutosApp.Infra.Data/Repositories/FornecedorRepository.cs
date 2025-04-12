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
    public class FornecedorRepository : IFornecedorRepository
    {
        public void Add(Fornecedor fornecedor)
        {
            using(var dataContext = new DataContext())
            {
                dataContext.Add(fornecedor);
                dataContext.SaveChanges();
            }
        }

        public void Update(Fornecedor fornecedor)
        {
            using(var dataContext = new DataContext())
            {
                dataContext.Update(fornecedor);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Fornecedor fornecedor)
        {
            using(var dataContext = new DataContext())
            {
                dataContext.Remove(fornecedor);
                dataContext.SaveChanges();
            }
        }

        public Fornecedor? GetById(Guid? id)
        {
            using (var dataContext = new DataContext())
            {
                //LAMBDA
                return dataContext.Set<Fornecedor>()
                    .Include(x => x.Produtos)
                    .SingleOrDefault(x => x.Id == id);
            }
        }
        
        public IEnumerable<Fornecedor> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                //LAMBDA
                return dataContext.Set<Fornecedor>()
                    .Include(x => x.Produtos)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Fornecedor? GetByName(string nome)
        {
            using (var dataContext = new DataContext())
            {
                //LAMBDA
                return dataContext.Set<Fornecedor>()
                    .SingleOrDefault(x => x.Nome == nome);
            }
        }
    }
}
