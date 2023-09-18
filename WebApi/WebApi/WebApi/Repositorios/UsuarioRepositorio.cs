using WebApi.Models;
using WebApi.Repositorios.Interfaces;

namespace WebApi.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public Usuario BuscarUsuarioPorLoginSenha(string login, string senha)
        {
            var usuarios = new List<Usuario>();

            usuarios.Add(new Usuario { Id = 1, Login = "adm@teste.com", NivelPermissao = Enumerados.NivelPermissao.Admin, Nome = "adm", Senha = "1234" });
            usuarios.Add(new Usuario { Id = 2, Login = "user@teste.com", NivelPermissao = Enumerados.NivelPermissao.Usuario, Nome = "user", Senha = "4321" });

            return usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senha);
        }
    }
}
