using ProdutosApp.Infra.Logging.Contexts;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Logging.Repositories
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly DataContext _dataContext = new DataContext();

        public void GravarLog_CadastroProduto(Logging_CadastroProduto log)
        {
            _dataContext.Log.InsertOne(log);
        }
    }
}
