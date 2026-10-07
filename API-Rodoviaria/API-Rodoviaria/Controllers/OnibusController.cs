using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces.IOnibus;
using API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
namespace API_Rodoviaria.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = NomesPerfis.Admin)]
public class OnibusController : ControllerBase
{
    private readonly ICriarOnibusCasoDeUso _criarOnibus;
    private readonly IListarOnibusCasoDeUso _listarOnibus;
    private readonly IVerOnibusPorIdCasoDeUso _obterOnibusPorId;
    private readonly IAtualizarOnibusCasoDeUso _atualizarOnibus;
    private readonly IDeletarOnibusCasoDeUso _deletarOnibus;
    public OnibusController(
    ICriarOnibusCasoDeUso criarOnibus,
    IListarOnibusCasoDeUso listarOnibus,
    IVerOnibusPorIdCasoDeUso obterOnibusPorId,
    IAtualizarOnibusCasoDeUso atualizarOnibus,
    IDeletarOnibusCasoDeUso deletarOnibus)
    {
        _criarOnibus = criarOnibus;
        _listarOnibus = listarOnibus;
        _obterOnibusPorId = obterOnibusPorId;
        _atualizarOnibus = atualizarOnibus;
        _deletarOnibus = deletarOnibus;
    }
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] RequisicaoCriarOnibusDTO requisicao)
    {
        var resposta = await _criarOnibus.ExecutarAsync(requisicao);
        return StatusCode(StatusCodes.Status201Created, resposta);
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery]PaginacaoRequisicao paginacao)
    {
        return Ok(await _listarOnibus.ExecutarAsync(paginacao));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return Ok(await _obterOnibusPorId.ExecutarAsync(id));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] RequisicaoCriarOnibusDTO requisicao)
    {
        return Ok(await _atualizarOnibus.ExecutarAsync(id, requisicao));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        await _deletarOnibus.ExecutarAsync(id);
        return NoContent();
    }

}
