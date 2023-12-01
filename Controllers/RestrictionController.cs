using ContactsManage.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManage.Controllers
{
    [PageLoggedInUser]
    public class RestrictionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
