using ProdutosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T objeto);

        void Update(T objeto);

        void Delete(T objeto);

        T? GetById(Guid? id);

        IEnumerable<T>? GetAll();
    }
}
