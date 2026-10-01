using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces.IMotorista;
using API_Rodoviaria.Application.DTO_s.Requisicao.MotoristaRequisicaoDTO;
namespace API_Rodoviaria.Controllers.Posts;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = NomesPerfis.Admin)]
public class MotoristasController : ControllerBase
{
    private readonly ICriarMotoristaCasoDeUso _criarMotorista;

    public MotoristasController(ICriarMotoristaCasoDeUso criarMotorista)
    {
        _criarMotorista = criarMotorista;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] RequisicaoCriarMotoristaDTO requisicao)
    {
        var resposta = await _criarMotorista.ExecutarAsync(requisicao);
        return StatusCode(StatusCodes.Status201Created, resposta);
    }
}
