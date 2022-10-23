using DawnPoets.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DawnPoets.Controllers
{
    [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
