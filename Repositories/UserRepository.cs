using ContactsManage.Data;
using ContactsManage.Models;

namespace ContactsManage.Repositorio
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext bancoContext)
        {
            this._context = bancoContext;
        }

        public User AddUser(User addUser)
        {
            addUser.RegisterDate = DateTime.Now;
            addUser.SetHashPassword();
            if (_context.Users != null) _context.Users.Add(addUser);

            _context.SaveChanges();

            return addUser;
        }

        public List<User> FindAll()
        {
            List<User> listUser = new List<User>();

            return _context.Users != null && _context.Users.Any() ? _context.Users.ToList() : listUser;
        }

        public User FindById(int id)
        {
            User trueUser = new User();

            if (_context.Users != null && _context.Users.Any())
            {
                foreach (User searchUser in _context.Users)
                {
                    if (searchUser.Id == id) trueUser = searchUser;
                }
            }

            return trueUser;
        }

        public User UpdateUser(User pUser)
        {
            User updateUser = FindById(pUser.Id);

            if (updateUser == null) throw new Exception("Houve um erro na atualização do usuário.");

            updateUser.Id = pUser.Id;
            updateUser.Name = pUser.Name;
            updateUser.Login = pUser.Login;
            updateUser.Email = pUser.Email;
            updateUser.Profile = pUser.Profile;

            if (_context.Users != null && _context.Users.Any()) _context.Users.Update(updateUser);

            _context.SaveChanges();

            return updateUser;
        }

        public bool DeleteUser(int id)
        {
            User deleteUser = FindById(id);

            if (deleteUser == null) throw new Exception("Houve um erro na Exclusão do usuário.");

            if (_context.Users != null && _context.Users.Any()) _context.Users.Remove(deleteUser);

            _context.SaveChanges();

            return true;
        }

        public User FindByLogin(string login)
        {
            return _context.Users.FirstOrDefault(user => user.Login.ToUpper() == login.ToUpper());
        }

        public User FindEmailLogin(string email, string login)
        {
            return _context.Users.FirstOrDefault(user => user.Email.ToUpper() == email.ToUpper() && user.Login.ToUpper() == login.ToUpper());
        }

        public User ChangePassword(ChangePassword changePasswordModel)
        {
            User userModel = FindById(changePasswordModel.Id);

            if (userModel == null) throw new Exception("Houve um erro na atualização da senha");

            if (!userModel.ValidPassword(changePasswordModel.CurrentPassword)) throw new Exception("Senha atual não confere");

            if (userModel.ValidPassword(changePasswordModel.NewPassword)) throw new Exception("Nova senha deve ser diferente da atual");

            userModel.SetNewPassword(changePasswordModel.NewPassword);
            userModel.UpdateDate = DateTime.Now;

            _context.Users.Update(userModel);
            _context.SaveChanges();
            return userModel;
        }
    }
}
