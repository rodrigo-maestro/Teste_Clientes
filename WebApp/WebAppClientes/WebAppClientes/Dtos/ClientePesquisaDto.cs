namespace WebAppClientes.Dtos
{
    public class ClientePesquisaDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int TotalResultados { get; set; }
    }
}
