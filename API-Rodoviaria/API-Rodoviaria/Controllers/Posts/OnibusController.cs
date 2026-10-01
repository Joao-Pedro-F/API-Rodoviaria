using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces.IOnibus;
using API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;
namespace API_Rodoviaria.Controllers.Posts;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = NomesPerfis.Admin)]
public class OnibusController : ControllerBase
{
    private readonly ICriarOnibusCasoDeUso _criarOnibus;
    public OnibusController(ICriarOnibusCasoDeUso criarOnibus)
    {
        _criarOnibus = criarOnibus;
    }
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] RequisicaoCriarOnibusDTO requisicao)
    {
        var resposta = await _criarOnibus.ExecutarAsync(requisicao);
        return StatusCode(StatusCodes.Status201Created, resposta);
    }
}
