using ContactsManage.Helper;
using ContactsManage.Models;
using ContactsManage.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManage.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly Helper.ISession _session;
        private readonly IEmail _email;

        public LoginController(IUserRepository usuarioRepositorio, Helper.ISession session, IEmail email)
        {
            _userRepository = usuarioRepositorio;
            _session = session;
            _email = email;
        }

        public IActionResult Index()
        {
            if (_session.FindSession() != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult UserLogout()
        {
            _session.RemoveSession();

            return RedirectToAction("Index", "Login");
        }

        public IActionResult RedefinePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendLinkRedefinePassword(RedefinePassword redefinePassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = _userRepository.FindEmailLogin(redefinePassword.Email, redefinePassword.Login);
                    if (user != null)
                    {
                        string newPassword = user.MakeNewPassword();

                        string messageEmail = $"<p style='color:black;'>Sua nova senha é:</p> {newPassword}";
                        bool sentEmail = _email.SendEmail(user.Email, "Contacts Manage - New Password", messageEmail);

                        if (sentEmail)
                        {
                            _userRepository.UpdateUser(user);
                            TempData["MsgSuccess"] = $"Enviamos para seu email cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MsgError"] = $"Não foi possível enviar o e-mail. Por favor, tente novamente.";
                        }
                        
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MsgError"] = $"Não foi possível redefinir sua senha. Por favor verifique os dados informados.";
                    return View("RedefinePassword");
                }
                return View("RedefinePassword");
            }
            catch (Exception)
            {

                TempData["MsgError"] = $"Ops!! Não foi possível redefinir sua senha, tente novamente ou entre em contato com o suporte.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult LoginAccess(LoginModel loginUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = _userRepository.FindByLogin(loginUser.Login);
                    if (user != null)
                    {
                        if (user.ValidPassword(loginUser.Password))
                        {
                            _session.CreateSession(user);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MsgError"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                    }
                    TempData["MsgError"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                }
                return View("Index");
            }
            catch (Exception ex)
            {

                TempData["MsgError"] = $"Ops!! Não foi possível realizar seu login, tente novamente ou entre em contato com o suporte, detalhes do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
