using System.Security.Claims;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Rodoviaria.Domain.Interfaces.IReserva;
using API_Rodoviaria.Application.DTO_s.Requisicao.ReservaRequisicaoDTO;
namespace API_Rodoviaria.Controllers.Posts;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReservasController : ControllerBase
{
    private readonly ICriarReservaCasoDeUso _criarReserva;
    public ReservasController(ICriarReservaCasoDeUso criarReserva)
    {
        _criarReserva = criarReserva;
    }
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] RequisicaoCriarReservaDTO requisicao)
    {
        var idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(idTexto, out var idUsuario))
        {
            return Unauthorized();
        }
        var resposta = await _criarReserva.ExecutarAsync(idUsuario, requisicao);
        return StatusCode(StatusCodes.Status201Created, resposta);
    }
}
