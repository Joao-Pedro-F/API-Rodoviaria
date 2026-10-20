using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.ViagemRequisicaoDTO;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.VerViagens;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces.ICadeira;
using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API_Rodoviaria.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ViagensController : ControllerBase
{
    private readonly ICriarViagemCasoDeUso _criarViagem;
    private readonly IListarViagensCasoDeUso _obterViagemPorId;
    private readonly IPaginacaoViagemCasoDeUso _listarviagem;
    private readonly IAtualizarViagemCasoDeUso _atualizarViagem;
    private readonly IDeletarViagemCasoDeUso _deletarViagem;
    public ViagensController(
    ICriarViagemCasoDeUso criarViagem,
    IListarViagensCasoDeUso obterViagemPorId,
    IPaginacaoViagemCasoDeUso listarviagem,
    IAtualizarViagemCasoDeUso atualizarViagem,
    IDeletarViagemCasoDeUso deletarViagem)
    {
        _criarViagem = criarViagem;
        _listarviagem = listarviagem;
        _obterViagemPorId = obterViagemPorId;
        _atualizarViagem = atualizarViagem;
        _deletarViagem = deletarViagem;
    }
    [HttpPost]
    [Authorize(Roles = NomesPerfis.Admin)]
    public async Task<IActionResult> Criar([FromBody]RequisicaoCriarViagemDTO request)
    {
        var resposta = await _criarViagem.ExecutarAsync(request);
        return StatusCode(StatusCodes.Status201Created, resposta);
    }
   
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Listar([FromQuery]PaginacaoRequisicao  paginacao)
    {
        return Ok(await _listarviagem.ExecutarAsync(paginacao));
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return Ok(await _obterViagemPorId.ExecutarAsync(id));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = NomesPerfis.Admin)]
    public async Task<IActionResult> Atualizar(int id, [FromBody]RequisicaoAtualizarViagemDTO request)
    {
        return Ok(await _atualizarViagem.ExecutarAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = NomesPerfis.Admin)]
    public async Task<IActionResult> Deletar(int id)
    {
        await _deletarViagem.ExecutarAsync(id);
        return NoContent();
    }


}
