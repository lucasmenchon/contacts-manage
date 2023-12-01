using ContactsManage.Data;
using ContactsManage.Models;

namespace ContactsManage.Repositorio
{
    public class ContactRepositorio : IContactRepositorio
    {
        private readonly DataContext _context;

        public ContactRepositorio(DataContext context)
        {
            this._context = context;
        }

        public Contact AddContact(Contact contact)
        {
            if (_context.Contacts != null) _context.Contacts.Add(contact);

            _context.SaveChanges();

            return contact;
        }

        public List<Contact> FindUserContactsById(int userId)
        {
            List<Contact> contacts = new List<Contact>();

            return _context.Contacts != null && _context.Contacts.Any() ? _context.Contacts.Where(contato => contato.UserId == userId).ToList() : contacts;
        }

        public Contact FindById(int id)
        {
            Contact newContact = new Contact();

            if (_context.Contacts != null && _context.Contacts.Any())
            {
                foreach (Contact contact in _context.Contacts)
                {
                    if (contact.Id == id) newContact = contact;
                }
            }

            return newContact;
        }

        public Contact UpdateContact(Contact contact)
        {
            Contact updatedContact = FindById(contact.Id);

            if (updatedContact == null)
                throw new Exception("Houve um erro na atualização do contato.");

            updatedContact.Id = updatedContact.Id;
            updatedContact.Name = contact.Name;
            updatedContact.Email = contact.Email;
            updatedContact.CellPhone = contact.CellPhone;

            if (_context.Contacts != null && _context.Contacts.Any()) _context.Contacts.Update(updatedContact);

            _context.SaveChanges();

            return updatedContact;
        }

        public bool DeleteContact(int id)
        {
            Contact contactDeleted = FindById(id);

            if (contactDeleted == null) throw new Exception("Houve um erro na Exclusão do contato.");

            if (_context.Contacts != null && _context.Contacts.Any()) _context.Contacts.Remove(contactDeleted);

            _context.SaveChanges();

            return true;
        }
    }
}
