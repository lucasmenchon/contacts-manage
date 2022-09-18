using DawnPoets.Models;
using DawnPoets.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace DawnPoets.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public IActionResult Index()
        {
            List<UserModel> users = _usuarioRepositorio.BuscarTodos();
            return View(users);
        }

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UserModel pUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pUser = _usuarioRepositorio.Adicionar(pUser);
                    TempData["MsgSuccess"] = $"Usuário {pUser.Nome} adicionado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View(pUser);
            }
            catch (Exception error)
            {
                TempData["MsgError"] = $"Ops!! Não foi possível cadastrar seu usuário, tente novamente ou entre em contato com o suporte, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
