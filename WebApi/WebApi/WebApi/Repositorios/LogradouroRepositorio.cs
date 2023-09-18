using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositorios.Interfaces;

namespace WebApi.Repositorios
{
    public class LogradouroRepositorio : ILogradouroRepositorio
    {
        private readonly WebApiDBContext _webApiDBContext;

        public LogradouroRepositorio(WebApiDBContext webApiDBContex)
        {
            _webApiDBContext = webApiDBContex;
        }

        private async Task<Logradouro> BuscarPorId(int id)
        {
            return await _webApiDBContext.Logradouros.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AdicionarLogradourosPorClienteProcedure(int idCliente, List<LogradouroDto> logradouros)
        {
            foreach (var logradouro in logradouros)
            {
                if (string.IsNullOrWhiteSpace(logradouro.Descricao))
                {
                    continue;
                }

                await AdicionarLogradouroProcedure(idCliente, logradouro.Descricao);
            }
        }

        public async Task<int> AdicionarLogradouroProcedure(int idCliente, string descricao)
        {
            var idClienteParam = new SqlParameter("@IdCliente", idCliente);
            var descricaoParam = new SqlParameter("@Descricao", descricao);
            var logradouroIdParam = new SqlParameter("@LogradouroId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            await _webApiDBContext.Database.ExecuteSqlRawAsync(
                "EXEC InserirLogradouro @IdCliente, @Descricao, @LogradouroId OUTPUT",
                idClienteParam, descricaoParam, logradouroIdParam);

            var logradouroId = (int)logradouroIdParam.Value;

            return logradouroId;
        }

        public async Task AlterarLogradouroProcedure(int id, string descricao)
        {
            await ValidarLogradouroExiste(id);

            var idParam = new SqlParameter("@Id", id);
            var descricaoParam = new SqlParameter("@Descricao", descricao);

            await _webApiDBContext.Database.ExecuteSqlRawAsync(
                "EXEC AlterarLogradouro @Id, @Descricao",
                idParam, descricaoParam
            );
        }

        public async Task ExcluirLogradouroProcedure(int id)
        {
            await ValidarLogradouroExiste(id);

            var idParam = new SqlParameter("@Id", id);

            await _webApiDBContext.Database.ExecuteSqlRawAsync(
                "EXEC ExcluirLogradouro @Id",
                idParam
            );
        }

        private async Task ValidarLogradouroExiste(int id)
        {
            var logradouroExistente = await BuscarPorId(id);

            if (logradouroExistente == null)
            {
                throw new Exception($"Logradouro com id {id} não encontrado");
            }
        }
    }
}
