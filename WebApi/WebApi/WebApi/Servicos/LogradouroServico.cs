using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositorios.Interfaces;
using WebApi.Servicos.Interfaces;

namespace WebApi.Servicos
{
    public class LogradouroServico : ILogradouroServico
    {
        private readonly ILogradouroRepositorio _logradouroRepositorio;

        public LogradouroServico(ILogradouroRepositorio logradouroRepositorio)
        {
            _logradouroRepositorio = logradouroRepositorio;
        }

        public async Task<int> Adicionar(NovoLogradouroDto novoLogradouro)
        {
            ValidarDtoAlteracaoLogradouro(novoLogradouro);

            if (novoLogradouro.IdCliente == 0)
            {
                throw new Exception("Logradouro precisa ter valor de Cliente");
            }

            return await _logradouroRepositorio.AdicionarLogradouroProcedure(novoLogradouro.IdCliente, novoLogradouro.Descricao);
        }

        public async Task Atualizar(AlteracaoLogradouroDto alteracaoLogradouro, int id)
        {
            ValidarDtoAlteracaoLogradouro(alteracaoLogradouro);

            await _logradouroRepositorio.AlterarLogradouroProcedure(id, alteracaoLogradouro.Descricao);
        }

        public async Task Apagar(int id)
        {
            await _logradouroRepositorio.ExcluirLogradouroProcedure(id);
        }

        private static void ValidarDtoAlteracaoLogradouro(AlteracaoLogradouroDto novoLogradouro)
        {
            if (novoLogradouro == null || string.IsNullOrWhiteSpace(novoLogradouro.Descricao))
            {
                throw new Exception("Logradouro precisa ter valor de descricao");
            }
        }
    }
}
