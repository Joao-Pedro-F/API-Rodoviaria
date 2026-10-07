using System.Security.Claims;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.ReservaRequisicaoDTO;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.VerReservas;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces.IReserva;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API_Rodoviaria.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReservasController : ControllerBase
{
    private readonly ICriarReservaCasoDeUso _criarReserva;
    private readonly IListarReservaCasoDeUso _listarReservas;
    private readonly IObterReservaPorId _obterReservaPorId;
    private readonly IAtualizarReservaCasoDeUso _atualizarReserva;
    private readonly IDeletarReservaCasoDeUso _deletarReserva;

    public ReservasController(
        ICriarReservaCasoDeUso criarReserva,
        IListarReservaCasoDeUso listarReservas,
        IObterReservaPorId obterReservaPorId,
        IAtualizarReservaCasoDeUso atualizarReserva,
        IDeletarReservaCasoDeUso deletarReserva)
    {
        _criarReserva = criarReserva;
        _listarReservas = listarReservas;
        _obterReservaPorId = obterReservaPorId;
        _atualizarReserva = atualizarReserva;
        _deletarReserva = deletarReserva;
    }

    private int IdUsuarioLogado()
    {
        var idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(idTexto, out var id) ? id : throw new
        UnauthorizedAccessException();
    }
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] RequisicaoCriarReservaDTO request)
    {
        var resposta = await _criarReserva.ExecutarAsync(IdUsuarioLogado(), request);
        return StatusCode(StatusCodes.Status201Created, resposta);
    }

    [HttpGet]
    [Authorize(Roles = NomesPerfis.Admin)]
    public async Task<IActionResult> Listar([FromQuery] PaginacaoRequisicao paginacao)
    {
        return Ok(await _listarReservas.ExecutarAsync(paginacao));
    }
    [HttpGet("{id:int}")]
    [Authorize(Roles = NomesPerfis.Admin)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return Ok(await _obterReservaPorId.ExecutarAsync(id));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody]RequisicaoAtualizarReservaDTO request)
    {
        return Ok(await
        _atualizarReserva.ExecutarAsync(IdUsuarioLogado(), id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        await _deletarReserva.ExecutarAsync(IdUsuarioLogado(), id);
        return NoContent();
    }

}
