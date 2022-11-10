using ContactsManage.Helper;
using ContactsManage.Models;
using ContactsManage.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManage.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public ChangePasswordController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(ChangePasswordModel changePasswordModel)
        {
            try
            {
                UserModel userLogged = _sessao.BuscarSessaoUsuario();
                changePasswordModel.Id = userLogged.Id;
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(changePasswordModel);
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
