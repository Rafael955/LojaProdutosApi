using MongoDB.Driver;
using ProdutosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Logging.Contexts
{
    public class DataContext
    {
        //atributos
        private readonly IMongoDatabase _mongoDatabase;

        //método construtor
        public DataContext()
        {
            var client = new MongoClient("mongodb://localhost:27018/");
            _mongoDatabase = client.GetDatabase("lojaprodutos_app");
        }

        public IMongoCollection<Logging_CadastroProduto> Log
        {
            get
            {
                return _mongoDatabase.GetCollection<Logging_CadastroProduto>("tb_logging");
            }
        }
    }
}
