using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosApp.Domain.Interfaces.Repositories;

namespace ProdutosApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public DashboardController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("obter-dados-fornecedores-produtos")]
        public IActionResult Get()
        {
            var result = _produtoRepository.GroupByFornecedor();

            return Ok(result);
        }

    }
}
