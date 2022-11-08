using DawnPoets.Filters;
using DawnPoets.Helper;
using DawnPoets.Models;
using DawnPoets.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace DawnPoets.Controllers
{
    [PageOnlyAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

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
            catch (Exception)
            {
                TempData["MsgError"] = $"Ops!! Não foi possível cadastrar seu usuário, tente novamente ou entre em contato com o suporte.";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UserModel deleteUser = _usuarioRepositorio.BuscarPorId(id);
            //TempData["msgDel"] = $"<script>window.alert('{contato.Nome}')</script>";
            return View(deleteUser);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                UserModel deleteUser = _usuarioRepositorio.BuscarPorId(id);
                if (deleteUser.Id == 0)
                {
                    TempData["MsgError"] = "Ops!! Este contato não existe para ser apagado.";
                    return RedirectToAction("Index");
                }

                _usuarioRepositorio.Apagar(deleteUser.Id);
                TempData["MsgSuccess"] = $"Usuário {deleteUser.Nome} apagado com sucesso.";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["MsgError"] = $"Ops!! Não foi possível apagar este usuário, tente novamente ou entre em contato com o suporte.";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            UserModel updateUser = _usuarioRepositorio.BuscarPorId(id);
            return View(updateUser);
        }

        [HttpPost]
        public IActionResult Alterar(UpdateUserModel pUpdateUser)
        {
            try
            {
                UserModel updateUser = null;

                //if (updateUser?.Id != pUserNoPw.Id)
                //{
                //    TempData["MsgError"] = "Ops!! Este Usuário não existe para ser atualizado.";
                //    return RedirectToAction("Index");
                //}

                if (ModelState.IsValid)
                {
                    updateUser = new UserModel()
                    {
                        Id = pUpdateUser.Id,
                        Nome = pUpdateUser.Nome,
                        Login = pUpdateUser.Login,
                        Email = pUpdateUser.Email,
                        Perfil = pUpdateUser.Perfil
                    };

                    updateUser = _usuarioRepositorio.Atualizar(updateUser);
                    TempData["MsgSuccess"] = $"Usuário {updateUser.Nome} atualizado com sucesso.";
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
