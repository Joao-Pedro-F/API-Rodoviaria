using System.Net;
using API_Rodoviaria.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_Rodoviaria.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ExcecaoDeNegocio negocio)
            {
                ApplicationException(context);
            }
            else
            {
                UnhandledExecption(context);
            }
        }

        private void ApplicationException(ExceptionContext context)
        {
            if (context.Exception is ExcecaoDeNegocio ex)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new { Erro = ex.Message, ex.StatusCode });
            }
        }

        private static void UnhandledExecption(ExceptionContext context)
        {

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new { Erro = "Erro interno" });
        }
    }
}
