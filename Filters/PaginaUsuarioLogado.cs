using Microsoft.AspNetCore.Mvc.Filters;

namespace DawnPoets.Filters
{
    public class PaginaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
