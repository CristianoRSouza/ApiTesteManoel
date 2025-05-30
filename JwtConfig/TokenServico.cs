using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Data.Entities;
using LojaManoelApi.Interfaces.Repositories;
using LojaManoelApi.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LojaManoelApi.JwtConfig
{
    public class TokenServico
    {
        private readonly string _settings;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IUsuarioRepositorio _repositorio;
        public TokenServico(IOptions<JwtConfiguracao> settings, IPasswordHasher<Usuario> passwordHasher,IUsuarioRepositorio usuarioRepositorio)
        {
            _passwordHasher = passwordHasher;
            _settings = settings.Value.Secret;
            _repositorio = usuarioRepositorio;

        }

        public string GenerateToken(LoginDto user)
        {
            var userAuthenticated = VerifyUser(user);
            return ConfigJwt(userAuthenticated);
        }

        private Usuario VerifyUser(LoginDto user)
        {
            var userEntity = _repositorio.ObterEmail(user.Email).Result;
            if (userEntity == null)
                throw new UsuarioNaoEncontrado("Usuário não encontrado");

            var userAuthenticated = _passwordHasher.VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password);
            if (userAuthenticated == PasswordVerificationResult.Failed)
                throw new CredenciasErradas("Senha Incorreta!");

            return userEntity;
        }

        private string ConfigJwt(Usuario userEntity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userEntity.Name),
                    new Claim(ClaimTypes.Role,userEntity.Papel.PapelToken)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
