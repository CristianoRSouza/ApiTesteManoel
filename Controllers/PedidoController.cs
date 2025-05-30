using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoServico _pedidoServico;

    public PedidoController(IPedidoServico pedidoServico)
    {
        _pedidoServico = pedidoServico;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> GetAll()
    {
        try
        {
            var pedidos = await _pedidoServico.GetAllAsync();
            return Ok(pedidos);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, title: "Erro ao buscar os pedidos.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoDto>> Get(int id)
    {
        try
        {
            var pedido = await _pedidoServico.GetByIdAsync(id);
            if (pedido == null)
                return NotFound($"Pedido com ID {id} não encontrado.");

            return Ok(pedido);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, title: $"Erro ao buscar o pedido com ID {id}.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<PedidoDto>> Add(PedidoDto pedido)
    {
        try
        {
            await _pedidoServico.AddAsync(pedido);

            return CreatedAtAction(nameof(Get), new { id = pedido.PedidoId }, pedido);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, title: "Erro ao criar o pedido.");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, PedidoDto pedido)
    {
        try
        {
            var pedidoExistente = await _pedidoServico.GetByIdAsync(id);
            if (pedidoExistente == null)
                return NotFound($"Pedido com ID {id} não encontrado.");

            await _pedidoServico.UpdateAsync(id, pedido);
            return Ok($"Pedido com ID {id} atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, title: $"Erro ao atualizar o pedido com ID {id}.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var result = await _pedidoServico.DeleteAsync(id);
            if (!result)
                return NotFound($"Pedido com ID {id} não encontrado.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, title: $"Erro ao excluir o pedido com ID {id}.");
        }
    }
}
