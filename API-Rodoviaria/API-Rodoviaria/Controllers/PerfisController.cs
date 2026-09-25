using API_Rodoviaria.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Rodoviaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfisController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CriarPerfil([FromServices] ICriarPerfilCasoDeUso criarPerfilCasoDeUso)
        {
            await criarPerfilCasoDeUso.Executar();
            return Ok();
        }
    }
}
