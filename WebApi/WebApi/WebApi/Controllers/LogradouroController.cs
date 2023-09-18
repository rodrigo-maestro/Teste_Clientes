using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Servicos.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogradouroController : ControllerBase
    {
        private readonly ILogradouroServico _logradouroServico;

        public LogradouroController(ILogradouroServico logradouroServico)
        {
            _logradouroServico = logradouroServico;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Logradouro>> Cadastrar([FromBody] NovoLogradouroDto novoLogradouro)
        {
            try
            {
                var id = await _logradouroServico.Adicionar(novoLogradouro);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Logradouro>> Atualizar([FromBody] AlteracaoLogradouroDto alteracaoLogradouro, int id)
        {
            try
            {
                await _logradouroServico.Atualizar(alteracaoLogradouro, id);
                return Ok();
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Apagar(int id)
        {
            try
            {
                await _logradouroServico.Apagar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
