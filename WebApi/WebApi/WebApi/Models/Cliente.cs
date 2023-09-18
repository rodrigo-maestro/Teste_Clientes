namespace WebApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? IdLogotipo { get; set; }

        public ICollection<Logradouro>? Logradouros { get; set; }
    }
}
