using LojaManoelApi.Data.Context;
using LojaManoelApi.Data.Entities;

namespace LojaManoelApi.JwtConfig
{
    public class JwtPapeis
    {
        private readonly AplicacaoContexto _contexto;

        public JwtPapeis(AplicacaoContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Papel> ObtemPapel(string papel)
        {
            return _contexto.Papeis
                .Where(p => p.PapelToken == papel)
                .FirstOrDefault()!;
        }
    }

}
