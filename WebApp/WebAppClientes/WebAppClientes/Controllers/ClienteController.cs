using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppClientes.Dtos;
using WebAppClientes.Services;

namespace WebAppClientes.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteApiService _clienteApiService;

        public ClienteController(ClienteApiService clienteApiService)
        {
            _clienteApiService = clienteApiService;
        }

        public async Task<IActionResult> ListarClientes()
        {
            string nome = null;
            int numPagina = 1;
            int qtdItensPagina = 50;

            var clientes = await _clienteApiService.GetClientesAsync(ObterToken(), nome, numPagina, qtdItensPagina);

            return View(clientes);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            try
            {
                var usuarioLogado = JsonConvert.DeserializeObject<UsuarioLogadoDto>(HttpContext.Session.GetString("AuthObject"));
                string token = usuarioLogado.Token;
                var clienteDetalhes = await _clienteApiService.ObterClienteDetalhesDaAPI(id, ObterToken());
                return View(clienteDetalhes);
            }
            catch (Exception ex) 
            {
                TempData["Erro"] = "Ocorreu um erro ao obter o cliente: " + ex.Message;
                return RedirectToAction("ListarClientes");
            }
        }

        public async Task<IActionResult> AdicionarCliente(ClienteAdicionarDto clienteDto)
        {
            try
            {
                if (clienteDto != null && !string.IsNullOrWhiteSpace(clienteDto.Nome) && !string.IsNullOrWhiteSpace(clienteDto.Email))
                {
                    await _clienteApiService.AdicionarClienteNaAPI(clienteDto, ObterToken());
                    return RedirectToAction("ListarClientes");
                }

                return View(clienteDto);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ocorreu um erro ao adicionar o cliente: " + ex.Message;
                return View(clienteDto);
            }
        }

        public async Task<IActionResult> ExcluirCliente(int id)
        {
            try
            {
                await _clienteApiService.ExcluirClienteNaAPI(id, ObterToken());
                return RedirectToAction("ListarClientes");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ocorreu um erro ao deletar o cliente: " + ex.Message;
                return RedirectToAction("ListarClientes");
            }
        }

        private string ObterToken()
        {
            var usuarioLogado = JsonConvert.DeserializeObject<UsuarioLogadoDto>(HttpContext.Session.GetString("AuthObject"));
            return usuarioLogado.Token;
        }
    }
}
