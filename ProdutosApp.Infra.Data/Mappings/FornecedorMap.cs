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
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("FORNECEDORES");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .HasColumnName("ID");

            builder.Property(f => f.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(f => f.Nome).IsUnique();

            //RELACIONAMENTOS TABELAS
            builder.HasMany(f => f.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p => p.FornecedorId);
        }
    }
}
