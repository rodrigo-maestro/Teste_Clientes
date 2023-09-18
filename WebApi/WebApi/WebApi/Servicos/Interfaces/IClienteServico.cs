using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Servicos.Interfaces
{
    public interface IClienteServico
    {
        Task<List<ClientePesquisaDto>> Pesquisar(string nome, int numPagina, int qtdItensPagina);

        Task<ClienteDto> BuscarDtoPorId(int id);

        Task<int> Adicionar(CriacaoClienteDto novoCliente);

        Task Atualizar(AlteracaoClienteDto alteracaoCliente, int id);

        Task Apagar(int id);
    }
}
