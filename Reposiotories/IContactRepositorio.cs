using ContactsManage.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ContactsManage.Repositorio
{
    public interface IContactRepositorio
    {
        Contact AddContact(Contact contato);

        List<Contact> FindUserContactsById(int userId);

        Contact FindById(int id);

        Contact UpdateContact(Contact contato);

        bool DeleteContact(int id);
    }
}
