using ContactsManage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ContactsManage.Filters
{
    public class PageLoggedInUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {        
            string userSession = context.HttpContext.Session.GetString("LoggedInUser");

            if (string.IsNullOrEmpty(userSession))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller" , "Login"}, {"action" , "Index" } });
            }
            else
            {
                User userLogin = JsonConvert.DeserializeObject<User>(userSession);

                if(userLogin == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }
            
            base.OnActionExecuting(context);
        }
    }
}
