using DawnPoets.Filters;
using DawnPoets.Models;
using DawnPoets.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace DawnPoets.Controllers
{
    [PaginaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MsgSuccess"] = $"Contato {contato.Nome} adicionado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception error)
            {
                TempData["MsgError"] = $"Ops!! Não foi possível cadastrar seu contato, tente novamente ou entre em contato com o suporte, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                ContatoModel lcontato = _contatoRepositorio.BuscarPorId(contato.Id);
                if (lcontato.Id != contato.Id)
                {
                    TempData["MsgError"] = "Ops!! Este contato não existe para ser atualizado.";
                    return RedirectToAction("Index");
                }
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MsgSuccess"] = $"Contato {contato.Nome} atualizado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch (Exception error)
            {
                TempData["MsgError"] = $"Ops!! Não foi possível atualizar seu contato, tente novamente ou entre em contato com o suporte, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                ContatoModel contatoDelete = _contatoRepositorio.BuscarPorId(id);
                if (contatoDelete.Id == 0)
                {
                    TempData["MsgError"] = "Ops!! Este contato não existe para ser apagado.";
                    return RedirectToAction("Index");
                }

                _contatoRepositorio.Apagar(contatoDelete.Id);
                TempData["MsgSuccess"] = $"Contato {contatoDelete.Nome} apagado com sucesso.";
                return RedirectToAction("Index");

            }
            catch (Exception error)
            {
                TempData["MsgError"] = $"Ops!! Não foi possível apagar seu contato, tente novamente ou entre em contato com o suporte, detalhes do erro: {error}";
                return RedirectToAction("Index");
            }
        }

    }
}
