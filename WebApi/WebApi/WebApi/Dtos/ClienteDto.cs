namespace WebApi.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? IdLogotipo { get; set; }
        public string? LinkLogotipo { get; set; }
        public List<LogradouroDto>? Logradouros { get; set; }
    }
}