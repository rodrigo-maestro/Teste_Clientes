using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositorios.Interfaces;

namespace WebApi.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly WebApiDBContext _webApiDBContext;

        public ClienteRepositorio(WebApiDBContext webApiDBContex)
        {
            _webApiDBContext = webApiDBContex;
        }

        public async Task<List<ClientePesquisaDto>> Pesquisar(string nome, int numPagina, int qtdItensPagina)
        {
            var query = _webApiDBContext.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(c => c.Nome.Contains(nome));
            }
                
            var totalResultados = await query.CountAsync();

            query = query.OrderBy(c => c.Nome);

            var clientesPaginados = await query
                .Skip((numPagina <= 0 ? 0 : numPagina - 1) * qtdItensPagina)
                .Take(qtdItensPagina)
                .ToListAsync();

            var resultadosDto = clientesPaginados.Select(c => new ClientePesquisaDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Email = c.Email,
                TotalResultados = totalResultados
            }).ToList();

            return resultadosDto;
        }


        public async Task<Cliente> BuscarPorId(int id)
        {
            return await _webApiDBContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ClienteDto> BuscarDtoPorId(int id)
        {
            var cliente = await _webApiDBContext.Clientes
                .Include(c => c.Logradouros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                return null;
            }

            return new ClienteDto()
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                IdLogotipo = cliente.IdLogotipo,
                Logradouros = cliente.Logradouros.Select(l => new LogradouroDto
                {
                    Id = l.Id,
                    Descricao = l.Descricao
                }).ToList()
            };
        }

        public async Task<int> AdicionarClienteProcedure(string nome, string email, string idLogotipo)
        {
            var nomeParam = new SqlParameter("@Nome", nome);
            var emailParam = new SqlParameter("@Email", email);
            var idLogotipoParam = new SqlParameter("@IdLogotipo", idLogotipo ?? (object)DBNull.Value);
            var clienteIdParam = new SqlParameter("@ClienteId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            await _webApiDBContext.Database.ExecuteSqlRawAsync(
                "EXEC InserirCliente @Nome, @Email, @IdLogotipo, @ClienteId OUTPUT",
                nomeParam, emailParam, idLogotipoParam, clienteIdParam);

            var clienteId = (int)clienteIdParam.Value;

            return clienteId;
        }

        public async Task AlterarClienteProcedure(int id, string nome, string email, string idLogotipo)
        {
            await ValidarClienteExiste(id);

            var idParam = new SqlParameter("@Id", id);
            var nomeParam = new SqlParameter("@Nome", nome);
            var emailParam = new SqlParameter("@Email", email);
            var idLogotipoParam = new SqlParameter("@IdLogotipo", idLogotipo);

            await _webApiDBContext.Database.ExecuteSqlRawAsync(
                "EXEC AlterarCliente @Id, @Nome, @Email, @IdLogotipo",
                idParam, nomeParam, emailParam, idLogotipoParam
            );
        }

        public async Task ExcluirClienteProcedure(int id)
        {
            await ValidarClienteExiste(id);

            var idParam = new SqlParameter("@Id", id);

            await _webApiDBContext.Database.ExecuteSqlRawAsync(
                "EXEC ExcluirCliente @Id",
                idParam
            );
        }

        private async Task ValidarClienteExiste(int id)
        {
            var clienteExistente = await BuscarPorId(id);

            if (clienteExistente == null)
            {
                throw new Exception($"Cliente com id {id} não encontrado");
            }
        }
    }
}
