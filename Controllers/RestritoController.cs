using ContactsManage.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManage.Controllers
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
