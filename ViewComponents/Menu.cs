using ContactsManage.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContactsManage.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userSession = HttpContext.Session.GetString("SessaoUsuarioLogado");

            if (string.IsNullOrEmpty(userSession))
            {
                return null;
            }

            UserModel user = JsonConvert.DeserializeObject<UserModel>(userSession);

            return View(user);
        }
    }
}
