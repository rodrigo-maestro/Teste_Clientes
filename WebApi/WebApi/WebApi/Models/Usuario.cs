using WebApi.Enumerados;

namespace WebApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Login { get; set; }
        public string? Senha { get; set; }
        public NivelPermissao NivelPermissao { get; set; }
    }
}
