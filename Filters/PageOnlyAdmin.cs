using DawnPoets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace DawnPoets.Filters
{
    public class PageOnlyAdmin : ActionFilterAttribute
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
                if(userLogin.Perfil != Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }
            
            base.OnActionExecuting(context);
        }
    }
}
