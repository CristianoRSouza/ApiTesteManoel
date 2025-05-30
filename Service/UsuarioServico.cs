using AutoMapper;
using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Data.Entities;
using LojaManoelApi.Interfaces.Repositories;
using LojaManoelApi.Interfaces.Services;
using LojaManoelApi.JwtConfig;
using Microsoft.AspNetCore.Identity;

namespace LojaManoelApi.Service
{
    public class UsuarioServico: IUsuarioServico
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly JwtPapeis _jwtPapeis;
        public UsuarioServico(IMapper mapper, IUsuarioRepositorio usuarioRepositorio, IPasswordHasher<Usuario> passwordHasher,
            JwtPapeis jwtPapeis)
        {
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
            _passwordHasher = passwordHasher;
            _jwtPapeis = jwtPapeis;
        }
        public async Task Adicionar(UsuarioDto usuario)
        {
            var usuarioEntidade = _mapper.Map<Usuario>(usuario);
            usuarioEntidade.PasswordHash = _passwordHasher.HashPassword(usuarioEntidade, usuario.Password);

            usuarioEntidade.Papel = await _jwtPapeis.ObtemPapel("Client");

            await _usuarioRepositorio.Adicionar(usuarioEntidade);
        }

        public async Task<bool> Deletar(int id)
        {
            await _usuarioRepositorio.Deletar(id);
            return true;
        }

        public async Task<IEnumerable<UsuarioDto>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<UsuarioDto>>(await _usuarioRepositorio.ObterTodos());
        }

        public async Task<UsuarioDto> ObterPorId(int id)
        {
            return _mapper.Map<UsuarioDto>(await _usuarioRepositorio.ObterPorId(id));
        }

        public async Task Atualizar(int id,UsuarioDto usuario)
        {
            var usuarioExistente = await _usuarioRepositorio.ObterPorId(usuario.Id);
            if (usuarioExistente == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {usuario.Id} não foi encontrado.");
            }

            _mapper.Map(usuario, usuarioExistente);


            await _usuarioRepositorio.Atualizar(_mapper.Map<Usuario>(usuarioExistente));
        }
    }
}
