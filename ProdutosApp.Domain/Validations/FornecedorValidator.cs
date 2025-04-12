using System;
using FluentValidation;
using ProdutosApp.Domain.Entities;

namespace ProdutosApp.Domain.Validations;

public class FornecedorValidator : AbstractValidator<Fornecedor>
{
    public FornecedorValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("O ID do fornecedor é obrigatório");

        RuleFor(p => p.Nome)
            .NotEmpty()
                .WithMessage("O nome do fornecedor é obrigatório.")
            .MaximumLength(100)
                .WithMessage("O nome do fornecedor deve ter no máximo 100 caracteres.")
            .MinimumLength(10)
                .WithMessage("O nome do fornecedor deve ter no mínimo 10 caracteres.");
    }
}
