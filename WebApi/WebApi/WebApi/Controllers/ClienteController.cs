using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Enumerados;
using WebApi.Models;
using WebApi.Servicos.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServico _clienteServico;

        public ClienteController(IClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ClientePesquisaDto>>> Pesquisar(string? nome, int numPagina, int qtdItensPagina)
        {
            try
            {
                var clientes = await _clienteServico.Pesquisar(nome, numPagina, qtdItensPagina);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task <ActionResult<ClienteDto>> BuscarDtoPorId(int id)
        {
            try
            {
                var cliente = await _clienteServico.BuscarDtoPorId(id);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> Cadastrar([FromBody] CriacaoClienteDto novoCliente)
        {
            try
            {
                var id = await _clienteServico.Adicionar(novoCliente);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Atualizar([FromBody] AlteracaoClienteDto alteracaoCliente, int id)
        {
            try
            {
                await _clienteServico.Atualizar(alteracaoCliente, id);
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
                await _clienteServico.Apagar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
