using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Front.Atributos
{
    public class ValidarSessao : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var logado = context.HttpContext.Session.GetString("logado");

            if (logado != "true")
            {
                context.Result = new RedirectToActionResult("loginView", "Login", null);
            }
        }
    }
}
