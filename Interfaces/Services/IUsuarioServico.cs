using LojaManoelApi.Data.Dtos;

namespace LojaManoelApi.Interfaces.Services
{
    public interface IUsuarioServico
    {
        Task<IEnumerable<UsuarioDto>> ObterTodos();
        Task<UsuarioDto?> ObterPorId(int id);
        Task Adicionar(UsuarioDto pedido);
        Task Atualizar(int id, UsuarioDto updatedPedido);
        Task<bool> Deletar(int id);
    }
}
