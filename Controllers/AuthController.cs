using LojaManoelApi.Data.Dtos;
using LojaManoelApi.JwtConfig;
using LojaManoelApi.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaManoelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenServico _tokenService;
        public AuthController(TokenServico tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginDto user)
        {
            try
            {
                var result = _tokenService.GenerateToken(user);
                return Ok(result);
            }
            catch (CredenciasErradas ex)
            {
                return Unauthorized(new RespostaDeErro
                {
                    Status = 401,
                    Mensagem = ex.Message
                });
            }
            catch (UsuarioNaoEncontrado ex)
            {
                return StatusCode(500, new RespostaDeErro
                {
                    Status = 401,
                    Mensagem = ex.Message
                });
            }
        }


    }
}
