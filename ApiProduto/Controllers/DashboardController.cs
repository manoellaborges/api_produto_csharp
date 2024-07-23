using ApiProduto.Models;
using ApiProduto.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiProduto.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public DashboardController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Produto> produtos = await _produtoService.GetAllAsync();
            
            var dashboardData = produtos
                .GroupBy(p => p.Tipo)
                .Select(g => new
                {
                    Tipo = g.Key,
                    Quantidade = g.Count(),
                    PrecoUnitarioMedio = g.Average(p => p.PrecoUnitario)
                });

            return Ok(dashboardData);
        }
    }
}
