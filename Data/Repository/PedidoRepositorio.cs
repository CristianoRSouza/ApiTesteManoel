using LojaManoelApi.Data.Context;
using LojaManoelApi.Data.Entities;
using LojaManoelApi.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LojaManoelApi.Data.Repository
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly AplicacaoContexto _context;

        public PedidoRepositorio(AplicacaoContexto context)
        {
            _context = context;
        }

        public async Task<Pedido?> ObterPorId(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Produtos)
                    .ThenInclude(pr => pr.Dimensao)
                .FirstOrDefaultAsync(p => p.PedidoId == id);
        }

        public async Task<IEnumerable<Pedido>> ObterTodos()
        {
            return await _context.Pedidos
                .Include(p => p.Produtos)
                    .ThenInclude(pr => pr.Dimensao)
                .ToListAsync();
        }

        public async Task Adicionar(Pedido entidade)
        {
            await _context.Pedidos.AddAsync(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Produtos)
                .FirstOrDefaultAsync(p => p.PedidoId == id);

            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Atualizar(Pedido entidade)
        {
             var existingPedido = await _context.Pedidos
            .Include(p => p.Produtos)
            .FirstOrDefaultAsync(p => p.PedidoId == entidade.PedidoId);

            if (existingPedido != null)
            {
                existingPedido.Produtos = entidade.Produtos;

                _context.Pedidos.Update(existingPedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
