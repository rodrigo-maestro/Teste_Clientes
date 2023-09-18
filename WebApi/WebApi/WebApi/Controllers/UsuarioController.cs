using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Dtos;
using WebApi.Repositorios.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UsuarioLoginDto usuarioLogin)
        {
            try
            {
                if (usuarioLogin == null || string.IsNullOrEmpty(usuarioLogin.Login))
                {
                    throw new Exception($"Informe um Usuário");
                }

                var usuario = _usuarioRepositorio.BuscarUsuarioPorLoginSenha(usuarioLogin.Login, usuarioLogin.Senha);

                if (usuario == null)
                {
                    throw new Exception($"Usuário {usuarioLogin.Login} não encontrado");
                }

                var token = TokenServico.GerarToken(usuario);

                var usuarioLogado = new UsuarioLogadoDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Token = token
                };

                return Ok(usuarioLogado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
