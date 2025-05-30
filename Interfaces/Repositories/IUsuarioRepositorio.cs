using LojaManoelApi.Data.Entities;

namespace LojaManoelApi.Interfaces.Repositories
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> ObterPorId(int id);
        Task<IEnumerable<Usuario>> ObterTodos();
        Task Adicionar(Usuario entidade);
        Task Deletar(int id);
        Task Atualizar(Usuario entidade);
        Task<Usuario> ObterEmail(string email);
    }

}
