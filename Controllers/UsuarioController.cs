using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Interfaces.Services;
using LojaManoelApi.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaManoelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var users = await _usuarioServico.ObterTodos();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RespostaDeErro
                {
                    Status = 500,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                var user = await _usuarioServico.ObterPorId(id);

                if (user == null)
                {
                    return NotFound(new RespostaDeErro
                    {
                        Status = 404,
                        Mensagem = $"Usuário com ID {id} não foi encontrado"
                    });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RespostaDeErro
                {
                    Status = 500,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UsuarioDto user)
        {
            try
             {
                await _usuarioServico.Adicionar(user);
                return CreatedAtAction(nameof(ObterPorId), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RespostaDeErro
                {
                    Status = 500,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Put([FromBody] UsuarioDto usuario)
        {
            try
            {
                var usuarioExistente = await _usuarioServico.ObterPorId(usuario.Id);

                if (usuarioExistente == null)
                {
                    return NotFound(new RespostaDeErro
                    {
                        Status = 404,
                        Mensagem = $"Usuário com ID {usuario.Id} não encontrado"
                    });
                }

                await _usuarioServico.Atualizar(usuario.Id,usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RespostaDeErro
                {
                    Status = 500,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _usuarioServico.ObterPorId(id);

                if (user == null)
                {
                    return NotFound(new RespostaDeErro
                    {
                        Status = 404,
                        Mensagem = $"Usuário com ID {id} não encontrado"
                    });
                }

                await _usuarioServico.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RespostaDeErro
                {
                    Status = 500,
                    Mensagem = ex.Message
                });
            }
        }
    }
}
