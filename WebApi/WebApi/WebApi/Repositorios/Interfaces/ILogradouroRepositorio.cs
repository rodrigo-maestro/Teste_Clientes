using WebApi.Dtos;

namespace WebApi.Repositorios.Interfaces
{
    public interface ILogradouroRepositorio
    {
        Task AdicionarLogradourosPorClienteProcedure(int idCliente, List<LogradouroDto> logradouros);

        Task<int> AdicionarLogradouroProcedure(int idCliente, string descricao);

        Task AlterarLogradouroProcedure(int id, string descricao);

        Task ExcluirLogradouroProcedure(int id);
    }
}
