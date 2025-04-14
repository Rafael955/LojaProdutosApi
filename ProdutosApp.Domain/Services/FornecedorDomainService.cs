using System;
using FluentValidation;
using ProdutosApp.Domain.Dtos.Requests;
using ProdutosApp.Domain.Dtos.Responses;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Domain.Interfaces.Services;
using ProdutosApp.Domain.Validations;

namespace ProdutosApp.Domain.Services
{
    public class FornecedorDomainService : IFornecedorDomainService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorDomainService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public FornecedorResponseDto CriarFornecedor(FornecedorRequestDto request)
        {
            #region Regra de Negócio: Fornecedor não poderá ter o mesmo nome de outro fornecedor

            var fornecedorSearch = _fornecedorRepository.GetByName(request.Nome);

            if (fornecedorSearch != null)
                throw new ApplicationException("Já existe um fornecedor cadastrado com este nome no sistema.");

            #endregion

            var fornecedor = new Fornecedor
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome
            };

            #region Validações dados do produto

            var supplierValidator = new FornecedorValidator();
            var results = supplierValidator.Validate(fornecedor);

            if (!results.IsValid)
                throw new ValidationException(results.Errors);
                
            #endregion

            _fornecedorRepository.Add(fornecedor);

            return new FornecedorResponseDto
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome
            };
        }

        public FornecedorResponseDto AlterarFornecedor(Guid? id, FornecedorRequestDto request)
        {
            #region Regra de Negócio: Fornecedor não poderá ter o mesmo nome de outro fornecedor

            var fornecedorSearch = _fornecedorRepository.GetByName(request.Nome);

            if (fornecedorSearch != null && fornecedorSearch.Id != id)
                throw new ApplicationException("Já existe um fornecedor cadastrado com este nome no sistema.");

            #endregion

            var fornecedor = _fornecedorRepository.GetById(id);

            fornecedor.Nome = request.Nome;
            
            #region Validações dados do produto

            var supplierValidator = new FornecedorValidator();
            var results = supplierValidator.Validate(fornecedor);

            if (!results.IsValid)
                throw new ValidationException(results.Errors);
                
            #endregion

            _fornecedorRepository.Update(fornecedor);

            return new FornecedorResponseDto
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome
            };
        }

        public FornecedorResponseDto ExcluirFornecedor(Guid? id)
        {
            var fornecedorSearch = _fornecedorRepository.GetById(id);

            if (fornecedorSearch == null)
                throw new ApplicationException("O fornecedor não foi encontrado!");

            #region Regra de Negócio: Não será possível excluir um fornecedor com produtos cadastrados em estoque

            if (fornecedorSearch.Produtos.Count > 0)
                throw new ApplicationException("Não será possível excluir este fornecedor pois ele possui produtos cadastrados no sistema");

            #endregion

            _fornecedorRepository.Delete(fornecedorSearch);

            return new FornecedorResponseDto
            {
                Id = fornecedorSearch.Id,
                Nome = fornecedorSearch.Nome
            };
        }

        public FornecedorResponseDto ObterFornecedorPorId(Guid? id)
        {
            var fornecedorSearch = _fornecedorRepository.GetById(id);

            if (fornecedorSearch == null)
                throw new ApplicationException("O fornecedor não foi encontrado!");

           FornecedorResponseDto fornecedorDto = new FornecedorResponseDto
            {
                Id = fornecedorSearch.Id,
                Nome = fornecedorSearch.Nome,
                Produtos = new List<ProdutoResponseDto>()
            };

            foreach (var produto in fornecedorSearch.Produtos)
            {
                fornecedorDto.Produtos.Add(new ProdutoResponseDto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco,
                    Quantidade = produto.Quantidade,
                    FornecedorId = id,
                    NomeFornecedor = fornecedorSearch.Nome
                });
            }

            return fornecedorDto;
        }

        public List<FornecedorResponseDto> ObterFornecedores()
        {
            List<FornecedorResponseDto> fornecedoresDto = new List<FornecedorResponseDto>();

            var fornecedores = _fornecedorRepository.GetAll();

            foreach (var fornecedor in fornecedores)
            {
                var _fornecedor = new FornecedorResponseDto
                {
                    Id = fornecedor.Id,
                    Nome = fornecedor.Nome,
                    Produtos = new List<ProdutoResponseDto>()
                };

                foreach (var produto in fornecedor.Produtos)
                {
                    _fornecedor.Produtos.Add(new ProdutoResponseDto
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        Quantidade = produto.Quantidade,
                        FornecedorId = _fornecedor.Id,
                        NomeFornecedor = _fornecedor.Nome
                    });
                }

                fornecedoresDto.Add(_fornecedor);
            }

            return fornecedoresDto;
        }
    }
}
