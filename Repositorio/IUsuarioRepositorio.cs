using DawnPoets.Models;

namespace DawnPoets.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UserModel BuscarPorLogin(string login);

        UserModel BuscarEmailLogin(string email, string login);
        
        UserModel Adicionar(UserModel user);

        List<UserModel> BuscarTodos();

        UserModel BuscarPorId(int id);

        UserModel Atualizar(UserModel user);

        bool Apagar(int id);
    }
}
