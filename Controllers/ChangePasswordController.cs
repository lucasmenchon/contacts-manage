using ContactsManage.Helper;
using ContactsManage.Models;
using ContactsManage.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManage.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly Helper.ISession _session;
        public ChangePasswordController(IUserRepository userRepository, Helper.ISession session)
        {
            _userRepository = userRepository;
            _session = session;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePasswordModel)
        {
            try
            {
                User userLogged = _session.FindSession();
                changePasswordModel.Id = userLogged.Id;
                if (ModelState.IsValid)
                {
                    _userRepository.ChangePassword(changePasswordModel);
                    TempData["MsgSuccess"] = $"Senha alterada com sucesso!";
                    return View("Index", changePasswordModel);
                }
                return View("Index", changePasswordModel);
            }
            catch (Exception error)
            {
                TempData["MsgError"] = $"Ops!! Não conseguimos alterar seua senha. Detalhe do erro: {error.Message}";
                return View("Index", changePasswordModel);
            }
        }
    }
}
