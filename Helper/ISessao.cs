using ContactsManage.Models;

namespace ContactsManage.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario(UserModel user);

        void RemoverSessaoUsuario();

        UserModel BuscarSessaoUsuario();
    }
}
