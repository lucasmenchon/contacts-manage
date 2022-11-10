using ContactsManage.Filters;
using ContactsManage.Helper;
using ContactsManage.Models;
using ContactsManage.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactsManage.Controllers
{
    [PaginaUsuarioLogado]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;       

        public HomeController(ILogger<HomeController> logger, ISessao sessao)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}