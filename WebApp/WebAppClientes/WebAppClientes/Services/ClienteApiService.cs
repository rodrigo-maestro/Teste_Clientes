using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using WebAppClientes.Dtos;

namespace WebAppClientes.Services
{
    public class ClienteApiService
    {
        private readonly HttpClient _httpClient;

        public ClienteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClientePesquisaDto>> GetClientesAsync(string token, string nome, int numPagina, int qtdItensPagina)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $"https://localhost:7294/api/Cliente?nome={nome}&numPagina={numPagina}&qtdItensPagina={qtdItensPagina}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var clientes = await response.Content.ReadFromJsonAsync<List<ClientePesquisaDto>>();
                return clientes;
            }
            else
            {
                throw new Exception($"Erro ao obter clientes: {response.StatusCode}");
            }
        }

        public async Task<ClienteDetalhesDto> ObterClienteDetalhesDaAPI(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $"https://localhost:7294/api/Cliente/{id}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var cliente = await response.Content.ReadFromJsonAsync<ClienteDetalhesDto>();
                return cliente;
            }
            else
            {
                throw new Exception($"Erro ao obter clientes: {response.StatusCode}");
            }
        }

        public async Task AdicionarClienteNaAPI(ClienteAdicionarDto clienteDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = "https://localhost:7294/api/Cliente";

            var clienteJson = JsonConvert.SerializeObject(clienteDto);

            var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // O cliente foi adicionado com sucesso
            }
            else
            {
                throw new Exception($"Erro ao adicionar cliente: {response.StatusCode}");
            }
        }

        public async Task ExcluirClienteNaAPI(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $"https://localhost:7294/api/Cliente/{id}";

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // O cliente foi excluido com sucesso
            }
            else
            {
                throw new Exception($"Erro ao adicionar cliente: {response.StatusCode}");
            }
        }
    }
}
