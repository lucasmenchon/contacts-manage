using ContactsManage.Models;

namespace ContactsManage.Helper
{
    public interface ISession
    {
        void CreateSession(User user);

        void RemoveSession();

        User FindSession();
    }
}
