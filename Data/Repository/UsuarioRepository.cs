using LojaManoelApi.Data.Context;
using LojaManoelApi.Data.Entities;
using LojaManoelApi.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LojaManoelApi.Data.Repository
{
    public class UsuarioRepositorio:IUsuarioRepositorio
    {
        protected readonly AplicacaoContexto _meuContexto;
        protected readonly DbSet<Usuario> _dbSet;

        public UsuarioRepositorio(AplicacaoContexto myContext)
        {
            _meuContexto = myContext;
            _dbSet = _meuContexto.Set<Usuario>();
        }

        public async Task<Usuario> ObterPorId(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Usuario>> ObterTodos() => await _dbSet.ToListAsync();

        public async Task Adicionar(Usuario entidade)
        {
            await _dbSet.AddAsync(entidade);
            _meuContexto.SaveChanges();
        }

        public async Task Deletar(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
            _meuContexto.SaveChanges();
        }

        public Task Atualizar(Usuario entidade)
        {
            _dbSet.Update(entidade);
            _meuContexto.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Usuario> ObterEmail(string email)
        {
            return await _dbSet.Include(p => p.Papel).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
