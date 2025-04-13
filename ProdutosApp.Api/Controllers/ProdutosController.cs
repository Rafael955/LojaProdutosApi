using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutosApp.Domain.Dtos.Requests;
using ProdutosApp.Domain.Dtos.Responses;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Services;
using ProdutosApp.Infra.Message.Models;
using ProdutosApp.Infra.Message.Producers;

namespace ProdutosApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoDomainService _produtoService;
        public ProdutosController(IProdutoDomainService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost("cadastrar-produto")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] ProdutoRequestDto request)
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                
                var response = _produtoService.CriarProduto(request);

                #region Cadastrando produto na fila da mensageria 

                var messageProducer = new MessageProducer();

                messageProducer.SendMessage(new ProdutoCriado
                {
                    Id = response.Id,
                    Nome = response.Nome,
                    Preco = response.Preco,
                    Quantidade = response.Quantidade,
                    Fornecedor = response.NomeFornecedor,
                    Usuario = userName,
                    CriadoEm = DateTime.Now
                });

                #endregion


                return StatusCode(StatusCodes.Status201Created, new 
                {
                    message = $"O produto {request.Nome} foi cadastrado com sucesso!",
                    data = response
                });
            }
            catch(ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new
                {
                    Name = e.PropertyName,
                    Error = e.ErrorMessage
                });

                return StatusCode(StatusCodes.Status400BadRequest, errors);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new 
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("alterar-produto/{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult Put(Guid? id, [FromBody] ProdutoRequestDto request)
        {
            try
            {
                var response = _produtoService.AlterarProduto(id, request);

                return StatusCode(StatusCodes.Status200OK, new 
                {
                    message = $"Os dados do produto {request.Nome} foram alterados com sucesso!",
                    data = response
                });
            }
            catch(ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new
                {
                    Name = e.PropertyName,
                    Error = e.ErrorMessage
                });

                return StatusCode(StatusCodes.Status400BadRequest, errors);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new 
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("excluir-produto/{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult Delete(Guid? id)
        {
            try
            {
                var response = _produtoService.ExcluirProduto(id);

                return StatusCode(StatusCodes.Status200OK, new 
                {
                    message = $"O produto {response.Nome} foi excluido com sucesso!",
                    data = response
                });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new 
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("obter-produto/{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult GetById(Guid? id)
        {
            try
            {
                var response = _produtoService.ObterProdutoPorId(id);

                return StatusCode(StatusCodes.Status200OK, new 
                {
                    data = response
                });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new 
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("listar-produtos")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                var response = _produtoService.ObterProdutos();

                return StatusCode(StatusCodes.Status200OK, new 
                {
                    data = response
                });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new 
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                {
                    message = ex.Message
                });
            }
        }
    }
}
