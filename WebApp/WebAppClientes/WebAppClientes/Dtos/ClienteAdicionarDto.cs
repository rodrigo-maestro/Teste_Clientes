namespace WebAppClientes.Dtos
{
    public class ClienteAdicionarDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string IdLogotipo { get; set; }
        public List<LogradouroDto> Logradouros { get; set; }

        public ClienteAdicionarDto()
        {
            Logradouros = new List<LogradouroDto>();
        }
    }
}
