using System;
using System.Data;
using FluentValidation;
using ProdutosApp.Domain.Entities;

namespace ProdutosApp.Domain.Validations;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("O ID do produto é obrigatório");

        RuleFor(p => p.Nome)
            .NotEmpty()
                .WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(150)
                .WithMessage("O nome do produto deve ter no máximo 150 caracteres.")
            .MinimumLength(3)
                .WithMessage("O nome do produto deve ter no mínimo 3 caracteres.");
        
        RuleFor(p => p.Preco)
            .NotEmpty()
                .WithMessage("O preço do produto é obrigatório")
            .PrecisionScale(10, 2, true)
                .WithMessage("O preço do produto deve ter no máximo 10 digitos");

        RuleFor(p => p.Quantidade)
            .NotEmpty()
                .WithMessage("A quantidade do produto é obrigatória");

        RuleFor(p => p.FornecedorId)
            .NotEmpty()
                .WithMessage("O fornecedor do produto é obrigatório");
    }
}
