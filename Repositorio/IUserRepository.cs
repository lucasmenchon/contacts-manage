using ContactsManage.Models;

namespace ContactsManage.Repositorio
{
    public interface IUserRepository
    {
        User FindByLogin(string login);

        User FindEmailLogin(string email, string login);
        
        User AddUser(User user);

        List<User> FindAll();

        User FindById(int id);

        User UpdateUser(User user);

        User ChangePassword(ChangePassword changePassword);

        bool DeleteUser(int id);
    }
}
