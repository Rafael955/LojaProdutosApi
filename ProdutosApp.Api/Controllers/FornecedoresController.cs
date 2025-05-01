using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosApp.Domain.Dtos.Requests;
using ProdutosApp.Domain.Interfaces.Services;

namespace ProdutosApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorDomainService _fornecedorService;

        public FornecedoresController(IFornecedorDomainService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [HttpPost("cadastrar-fornecedor")]
        [ProducesResponseType(typeof(FornecedorRequestDto), StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] FornecedorRequestDto request)
        {
            try
            {
                var response = _fornecedorService.CriarFornecedor(request);

                return StatusCode(StatusCodes.Status201Created, new
                {
                    message = $"O fornecedor {response.Nome} foi cadastrado com sucesso!",
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

        [HttpPut("alterar-fornecedor/{id}")]
        [ProducesResponseType(typeof(FornecedorRequestDto), StatusCodes.Status200OK)]
        public IActionResult Put(Guid? id, [FromBody] FornecedorRequestDto request)
        {
            try
            {
                var response = _fornecedorService.AlterarFornecedor(id, request);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = $"Os dados do fornecedor {response.Nome} foram alterados com sucesso!",
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

        [HttpDelete("excluir-fornecedor/{id}")]
        [ProducesResponseType(typeof(FornecedorRequestDto), StatusCodes.Status200OK)]
        public IActionResult Delete(Guid? id)
        {
            try
            {
                var response = _fornecedorService.ExcluirFornecedor(id);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = $"O fornecedor {response.Nome} foi excluído com sucesso!",
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

        [HttpGet("obter-fornecedor/{id}")]
        [ProducesResponseType(typeof(FornecedorRequestDto), StatusCodes.Status200OK)]
        public IActionResult GetById(Guid? id)
        {
            try
            {
                var response = _fornecedorService.ObterFornecedorPorId(id);

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

        [HttpGet("listar-fornecedores")]
        [ProducesResponseType(typeof(FornecedorRequestDto), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                var response = _fornecedorService.ObterFornecedores();

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
