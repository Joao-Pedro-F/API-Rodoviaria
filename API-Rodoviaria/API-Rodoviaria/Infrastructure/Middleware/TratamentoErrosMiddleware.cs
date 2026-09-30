using API_Rodoviaria.Domain.Exceptions;

namespace API_Rodoviaria.Infrastructure.Middleware
{
    public class TratamentoErrosMiddleware
    {

        private readonly RequestDelegate _proximo;
        public TratamentoErrosMiddleware(RequestDelegate proximo)
        {
            _proximo = proximo;
        }
        public async Task InvokeAsync(HttpContext contexto)
        {
            try
            {
                await _proximo(contexto);
            }
            catch (ExcecaoDeNegocio ex)
            {
                contexto.Response.StatusCode = ex.StatusCode;
                contexto.Response.ContentType = "application/json";
                await contexto.Response.WriteAsJsonAsync(new { erro = ex.Message });
            }

        }
}
