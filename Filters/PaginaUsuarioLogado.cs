using ContactsManage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ContactsManage.Filters
{
    public class PaginaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {        
            string userSession = context.HttpContext.Session.GetString("SessaoUsuarioLogado");

            if (string.IsNullOrEmpty(userSession))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller" , "Login"}, {"action" , "Index" } });
            }
            else
            {
                UserModel userLogin = JsonConvert.DeserializeObject<UserModel>(userSession);

                if(userLogin == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }
            
            base.OnActionExecuting(context);
        }
    }
}
