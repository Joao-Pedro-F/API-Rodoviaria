using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Front.Atributos
{
    public class ValidarSessao : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var logado = context.HttpContext.Session.GetString("logado");
            var perfil = context.HttpContext.Session.GetString("Usuario");

            if (logado != "true")
            {
                context.Result = new RedirectToActionResult("loginView", "Login", null);
            }

            var actionName = context.RouteData.Values["action"]?.ToString();

            if (perfil == "Cliente" && actionName != "viagensViewUser")
            {
                context.Result = new RedirectToActionResult("viagensViewUser", "Home", null);
                return;
            } 
        }
    }
}
