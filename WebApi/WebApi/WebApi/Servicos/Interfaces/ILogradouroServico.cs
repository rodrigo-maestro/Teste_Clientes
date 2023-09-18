using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Servicos.Interfaces
{
    public interface ILogradouroServico
    {
        Task<int> Adicionar(NovoLogradouroDto novoLogradouro);

        Task Atualizar(AlteracaoLogradouroDto alteracaoLogradouro, int id);

        Task Apagar(int id);
    }
}
