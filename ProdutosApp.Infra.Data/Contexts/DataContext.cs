using Microsoft.EntityFrameworkCore;
using ProdutosApp.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        //método para configurar a string de conexão com o banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //adicionando a connectionString
            optionsBuilder.UseSqlServer("Data Source=\"localhost, 1434\";Initial Catalog=master;User ID=sa;Password=BatatinhaFrita123$;Encrypt=False");
        }

        //método para adionar cada classe de mapeamento do projeto
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //incluindo cada classe 'Map' no projeto
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
        }
    }
}