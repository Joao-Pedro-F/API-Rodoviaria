using API_Rodoviaria.Domain.Interfaces.IPerfil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Application.UseCase.PerfilCasosDeUso.CriarPerfil;

namespace API_Rodoviaria.Controllers.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfisController : ControllerBase
    {
        private readonly ICriarPerfilCasoDeUso _criarPerfil;
        public PerfisController(ICriarPerfilCasoDeUso criarPerfil)
        {
            _criarPerfil=criarPerfil;
        }
        [HttpPost]
        [Authorize(Roles = NomesPerfis.Admin)]
        public async Task<IActionResult> Criar([FromBody] RequisicaoCriarPerfilDTO requisicao)
        {
            var resposta = await _criarPerfil.ExecutarAsync(requisicao);
            return StatusCode(StatusCodes.Status201Created,resposta);
        }
       
    }
}
