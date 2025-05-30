using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Data.Entities;

namespace LojaManoelApi.Interfaces.Services
{
    public interface IPedidoServico
    {
        Task<IEnumerable<PedidoDto>> GetAllAsync();
        Task<PedidoDto?> GetByIdAsync(int id);
        Task AddAsync(PedidoDto pedido);
        Task UpdateAsync(int id, PedidoDto updatedPedido);
        Task<bool> DeleteAsync(int id);
    }
}
