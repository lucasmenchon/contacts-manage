using ContactsManage.Filters;
using ContactsManage.Helper;
using ContactsManage.Models;
using ContactsManage.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManage.Controllers
{
    [PageOnlyAdmin]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly Helper.ISession _sessao;

        public IActionResult Index()
        {
            List<User> users = _userRepository.FindAll();
            return View(users);
        }

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user = _userRepository.AddUser(user);
                    TempData["MsgSuccess"] = $"Usuário {user.Name} adicionado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception)
            {
                TempData["MsgError"] = $"Ops!! Não foi possível cadastrar seu usuário, tente novamente ou entre em contato com o suporte.";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ConfirmDelete(int id)
        {
            User deleteUser = _userRepository.FindById(id);
            
            return View(deleteUser);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                User deleteUser = _userRepository.FindById(id);
                if (deleteUser.Id == 0)
                {
                    TempData["MsgError"] = "Ops!! Este contato não existe para ser apagado.";
                    return RedirectToAction("Index");
                }

                _userRepository.DeleteUser(deleteUser.Id);
                TempData["MsgSuccess"] = $"Usuário {deleteUser.Name} apagado com sucesso.";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["MsgError"] = $"Ops!! Não foi possível apagar este usuário, tente novamente ou entre em contato com o suporte.";
                return RedirectToAction("Index");
            }
        }

        public IActionResult UpdateUser(int id)
        {
            User updateUser = _userRepository.FindById(id);
            return View(updateUser);
        }

        [HttpPost]
        public IActionResult UpdateUser(UpdateUser user)
        {
            try
            {
                User updateUser = null;
                
                if (ModelState.IsValid)
                {
                    updateUser = new User()
                    {
                        Id = user.Id,
                        Name = user.Nome,
                        Login = user.Login,
                        Email = user.Email,
                        Profile = user.Perfil
                    };

                    updateUser = _userRepository.UpdateUser(updateUser);
                    TempData["MsgSuccess"] = $"Usuário {updateUser.Name} atualizado com sucesso.";
                    return RedirectToAction("Index");
                }
                
                return View("Editar", updateUser);
            }
            catch (Exception)
            {
                TempData["MsgError"] = $"Ops!! Não foi possível atualizar o usuário, tente novamente ou entre em contato com o suporte.";
                return RedirectToAction("Index");
            }
        }
    }
}
