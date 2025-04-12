using FluentValidation;
using ProdutosApp.Domain.Dtos.Requests;
using ProdutosApp.Domain.Dtos.Responses;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Domain.Interfaces.Services;
using ProdutosApp.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Services
{
    public class ProdutoDomainService : IProdutoDomainService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoDomainService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public ProdutoResponseDto CriarProduto(ProdutoRequestDto request)
        {
            #region Regra de Negócio: Produto não poderá ter o mesmo nome de outro produto

            var produto = _produtoRepository.GetByName(request.Nome);

            if (produto != null)
                throw new ApplicationException("Já existe um produto com este nome cadastrado no sistema!");

            #endregion

            #region Regra de Negócio: Produto não poderá ter preço negativo

            if (request.Preco < 0)
                throw new ApplicationException("Preço do produto não poderá ser negativo!");

            #endregion

            #region Regra de Negócio: Produto não poderá ter quantidade negativa

            if (request.Quantidade < 0)
                throw new ApplicationException("Quantidade do produto não poderá ser negativa!");

            #endregion

            produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Preco = request.Preco,
                Quantidade = request.Quantidade,
                FornecedorId = request.FornecedorId
            };

            #region Validações dados do produto

            var productValidator = new ProdutoValidator();
            var results = productValidator.Validate(produto);

            if (!results.IsValid)
                throw new ValidationException(results.Errors);
                
            #endregion

            _produtoRepository.Add(produto);

            produto = _produtoRepository.GetById(produto.Id);

            return RetornaProdutoResponseDto(produto);
        }

        public ProdutoResponseDto AlterarProduto(Guid? id, ProdutoRequestDto request)
        {
            #region Regra de Negócio: Produto não poderá ter o mesmo nome de outro produto

            var produtoSearch = _produtoRepository.GetByName(request.Nome);

            if (produtoSearch != null && produtoSearch.Id != id)
                throw new ApplicationException("Já existe um produto com este nome cadastrado no sistema!");

            #endregion

            #region Regra de Negócio: Produto não poderá ter preço negativo

            if (request.Preco < 0)
                throw new ApplicationException("Preço do produto não poderá ser negativo!");

            #endregion

            #region Regra de Negócio: Produto não poderá ter quantidade negativa

            if (request.Quantidade < 0)
                throw new ApplicationException("Quantidade do produto não poderá ser negativa!");

            #endregion

            var produto = _produtoRepository.GetById(id);

            if (produto == null)
                throw new ApplicationException("O produto não foi encontrado!");

            produto.Nome = request.Nome;
            produto.Preco = request.Preco;
            produto.Quantidade = request.Quantidade;
            produto.FornecedorId = request.FornecedorId;
            produto.Fornecedor = null;

            #region Validações dados do produto

            var productValidator = new ProdutoValidator();
            var results = productValidator.Validate(produto);

            if (!results.IsValid)
                throw new ValidationException(results.Errors);
                
            #endregion

            _produtoRepository.Update(produto);
            
            produto = _produtoRepository.GetById(id);

            return RetornaProdutoResponseDto(produto);
        }

        public ProdutoResponseDto ExcluirProduto(Guid? id)
        {
            var produto = _produtoRepository.GetById(id);

            if (produto == null)
                throw new ApplicationException("O produto não foi encontrado!");

            var response = ObterProdutoPorId(id);

            #region Regra de Negócio: Não será possível excluir produtos com quantidade em estoque

            if (response.Quantidade > 0)
                throw new ApplicationException("Não será possível excluir este produto pois ele possui quantidade em estoque");

            #endregion

            _produtoRepository.Delete(produto);

            return response;
        }

        public ProdutoResponseDto ObterProdutoPorId(Guid? id)
        {
            var produto = _produtoRepository.GetById(id);

            if (produto == null)
                throw new ApplicationException("O produto não foi encontrado!");

            return RetornaProdutoResponseDto(produto);
        }

        public List<ProdutoResponseDto> ObterProdutos()
        {
            List<ProdutoResponseDto> produtosDto = new List<ProdutoResponseDto>();

            var produtos = _produtoRepository.GetAll();

            foreach (var produto in produtos)
                produtosDto.Add(RetornaProdutoResponseDto(produto));

            return produtosDto;
        }

        private ProdutoResponseDto RetornaProdutoResponseDto(Produto produto)
        {
            return new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                FornecedorId = produto.FornecedorId,
                NomeFornecedor = produto.Fornecedor.Nome,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade
            };
        }
    }
}
