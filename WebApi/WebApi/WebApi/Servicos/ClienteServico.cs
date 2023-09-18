using WebApi.Dtos;
using WebApi.IO;
using WebApi.Models;
using WebApi.Repositorios.Interfaces;
using WebApi.Servicos.Interfaces;

namespace WebApi.Servicos
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ILogradouroRepositorio _logradouroRepositorio;

        public ClienteServico(
            IClienteRepositorio clienteRepositorio, 
            ILogradouroRepositorio logradouroRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _logradouroRepositorio = logradouroRepositorio;
        }

        public async Task<List<ClientePesquisaDto>> Pesquisar(string nome, int numPagina, int qtdItensPagina)
        {
            return await _clienteRepositorio.Pesquisar(nome, numPagina, qtdItensPagina);
        }

        public async Task<ClienteDto> BuscarDtoPorId(int id)
        {
            var cliente = await _clienteRepositorio.BuscarDtoPorId(id);

            BuscarLogotipo(cliente);

            return cliente;
        }

        private void BuscarLogotipo(ClienteDto cliente)
        {
            if (cliente == null || string.IsNullOrWhiteSpace(cliente.IdLogotipo))
            {
                return;
            }

            cliente.LinkLogotipo = ArquivoProvider.ObterLinkImagemLogotipo(cliente.IdLogotipo);
        }

        public async Task<int> Adicionar(CriacaoClienteDto novoCliente)
        {
            ValidarDtoAlteracaoCliente(novoCliente);

            var idCliente = await _clienteRepositorio.AdicionarClienteProcedure(novoCliente.Nome, novoCliente.Email, novoCliente.IdLogotipo);

            await AdicionarLogradouros(novoCliente.Logradouros, idCliente);

            return idCliente;
        }

        public async Task Atualizar(AlteracaoClienteDto alteracaoCliente, int id)
        {
            ValidarDtoAlteracaoCliente(alteracaoCliente);

            await _clienteRepositorio.AlterarClienteProcedure(id, alteracaoCliente.Nome, alteracaoCliente.Email, alteracaoCliente.IdLogotipo);
        }

        private async Task AdicionarLogradouros(List<LogradouroDto> logradouros, int idCliente)
        {
            if (logradouros == null)
            {
                return;
            }
            
            var novosLogradouros = logradouros.Where(l => l.Id == 0).ToList();

            if (novosLogradouros.Any())
            {
                await _logradouroRepositorio.AdicionarLogradourosPorClienteProcedure(idCliente, logradouros);
            }
        }

        public async Task Apagar(int id)
        {
            await _clienteRepositorio.ExcluirClienteProcedure(id);
        }

        private static void ValidarDtoAlteracaoCliente(AlteracaoClienteDto alteracaoCliente)
        {
            if (alteracaoCliente == null || string.IsNullOrWhiteSpace(alteracaoCliente.Nome) || string.IsNullOrWhiteSpace(alteracaoCliente.Email))
            {
                throw new Exception("Cliente precisa ter valor de nome e e-mail");
            }
        }
    }
}
