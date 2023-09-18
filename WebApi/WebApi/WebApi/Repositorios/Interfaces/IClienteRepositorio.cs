using WebApi.Dtos;

namespace WebApi.Repositorios.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<ClientePesquisaDto>> Pesquisar(string nome, int numPagina, int qtdItensPagina);

        Task<ClienteDto> BuscarDtoPorId(int id);

        Task<int> AdicionarClienteProcedure(string nome, string email, string idLogotipo);

        Task AlterarClienteProcedure(int id, string nome, string email, string idLogotipo);

        Task ExcluirClienteProcedure(int id);
    }
}
