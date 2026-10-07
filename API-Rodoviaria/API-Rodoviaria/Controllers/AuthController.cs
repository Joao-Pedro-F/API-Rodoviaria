using System.Diagnostics.Contracts;
using API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using API_Rodoviaria.Domain.Interfaces.IUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Rodoviaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        
            public const string NomeCookie = "access_token";
        private readonly ILoginCasoDeUso _login;
        public AuthController(ILoginCasoDeUso login)
        {
            _login = login;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] RequisicaoLoginDTO
        requisicao)
        {
            var resposta = await _login.ExecutarAsync(requisicao);
           
        Response.Cookies.Append(NomeCookie, resposta.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = Request.IsHttps,
            SameSite = SameSiteMode.Strict,
            Expires = new DateTimeOffset(resposta.ExpiraEm, TimeSpan.Zero)
        });
            return Ok(resposta);
}
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(NomeCookie);
            return NoContent();
        }
    }
}
