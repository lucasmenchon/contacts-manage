using DawnPoets.Models;
using DawnPoets.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace DawnPoets.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    if (user != null)
                    {
                        if (user.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MsgError"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                    }
                    TempData["MsgError"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                }
                return View("Index");
            }
            catch (Exception error)
            {

                TempData["MsgError"] = $"Ops!! Não foi possível realizar seu login, tente novamente ou entre em contato com o suporte, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
