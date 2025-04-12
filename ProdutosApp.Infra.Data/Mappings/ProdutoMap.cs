using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTOS");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("ID");

            builder.Property(p => p.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Preco)
                .HasColumnName("PRECO")
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(p => p.Quantidade)
                .HasColumnName("QUANTIDADE")
                .IsRequired();

            builder.Property(p => p.FornecedorId)
                .HasColumnName("FORNECEDOR_ID")
                .IsRequired();

            builder.HasIndex(p => p.Nome).IsUnique();
        }
    }
}
