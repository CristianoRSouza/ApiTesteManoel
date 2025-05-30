using LojaManoelApi.Data.Entities;

namespace LojaManoelApi.Interfaces.Repositories
{
    public interface IPedidoRepositorio
    {
        Task<Pedido> ObterPorId(int id);
        Task<IEnumerable<Pedido>> ObterTodos();
        Task Adicionar(Pedido entidade);
        Task Deletar(int Id);
        Task Atualizar(Pedido entidade);
    }
}
