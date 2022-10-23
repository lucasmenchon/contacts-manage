using DawnPoets.Models;

namespace DawnPoets.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario(UserModel user);

        void RemoverSessaoUsuario();

        UserModel BuscarSessaoUsuario();
    }
}
