namespace WebApi.Dtos
{
    public class CriacaoClienteDto : AlteracaoClienteDto
    {
        public List<LogradouroDto>? Logradouros { get; set; }
    }
}
