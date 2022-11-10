using ContactsManage.Models;

namespace ContactsManage.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UserModel BuscarPorLogin(string login);

        UserModel BuscarEmailLogin(string email, string login);
        
        UserModel Adicionar(UserModel user);

        List<UserModel> BuscarTodos(); // deixar sem parametro faz realizar a busca de todos

        UserModel BuscarPorId(int id);

        UserModel Atualizar(UserModel user);

        UserModel AlterarSenha(ChangePasswordModel changePasswordModel);

        bool Apagar(int id);
    }
}
