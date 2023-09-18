using WebApi.Models;

namespace WebApi.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario BuscarUsuarioPorLoginSenha(string login, string senha);
    }
}
