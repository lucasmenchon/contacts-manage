using DawnPoets.Models;

namespace DawnPoets.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UserModel Adicionar(UserModel user);

        List<UserModel> BuscarTodos();

        UserModel BuscarPorId(int id);

        UserModel Atualizar(UserModel user);

        bool Apagar(int id);
    }
}
