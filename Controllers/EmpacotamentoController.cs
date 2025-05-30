using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Interfaces.Services;
using LojaManoelApi.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LojaManoelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpacotamentoController : ControllerBase
    {
        private readonly IEmpacotamentoServico _empacotamentoService;

        public EmpacotamentoController(IEmpacotamentoServico empacotamentoServico)
        {
            _empacotamentoService = empacotamentoServico;
        }

        [HttpPost]
        public IActionResult Empacotar([FromBody] List<PedidoDto> pedidos)
        {
            try
            {
                if (pedidos == null || !pedidos.Any())
                    return BadRequest(new { message = "A lista de pedidos não pode ser vazia." });

                var resultado = new List<object>();

                foreach (var pedido in pedidos)
                {
                    var caixas = _empacotamentoService.EmpacotarProdutos(pedido.Produtos);

                    resultado.Add(new
                    {
                        pedido_id = pedido.PedidoId,
                        caixas = caixas.Select(c => new
                        {
                            caixa_id = c.CaixaId,
                            produtos = c.Produtos.Select(p => p.ProdutoId).ToList()
                        }).ToList()
                    });
                }

                return Ok(new { pedidos = resultado });
            }
            catch (CaixaForaDoPadrao ex)
            {
                return StatusCode(500, new RespostaDeErro
                {
                    Mensagem = ex.Message,
                    Status = 500
                });
            }
        }
    }
}
