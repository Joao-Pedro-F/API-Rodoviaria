using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Requisicao.MotoristaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.UseCase.MotoristaCasosDeUso.VerMotorista;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces.IMotorista;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API_Rodoviaria.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = NomesPerfis.Admin)]
public class MotoristasController : ControllerBase
{
    private readonly ICriarMotoristaCasoDeUso _criarMotorista;
    private readonly IVerMotoristaPaginadoCasoDeUso _listarMotoristas;
    private readonly IVerMotoristaPorIdCasoDeUso _obterMotoristaPorId;
    private readonly IAtualizarMotoristaCasoDeUso _atualizarMotorista;
    private readonly IDeletarMotoristaCasoDeUso _deletarMotorista;


    public MotoristasController(
        ICriarMotoristaCasoDeUso criarMotorista,
        IVerMotoristaPaginadoCasoDeUso listarMotoristas,
        IVerMotoristaPorIdCasoDeUso obterMotoristaPorId,
        IAtualizarMotoristaCasoDeUso atualizarMotorista,
        IDeletarMotoristaCasoDeUso deletarMotorista)
    {
        _criarMotorista = criarMotorista;
        _listarMotoristas = listarMotoristas;
        _obterMotoristaPorId = obterMotoristaPorId;
        _atualizarMotorista = atualizarMotorista;
        _deletarMotorista = deletarMotorista;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody]RequisicaoCriarMotoristaDTO request)
    {
        var resposta = await _criarMotorista.ExecutarAsync(request);
        return StatusCode(StatusCodes.Status201Created, resposta);
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery]PaginacaoRequisicao paginacao)
    {
        return Ok(await _listarMotoristas.ExecutarAsync(paginacao));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return Ok(await _obterMotoristaPorId.ExecutarAsync(id));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody]RequisicaoAtualizarMotoristaDTO request)
    {
        return Ok(await _atualizarMotorista.Executar(id,
        request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        await _deletarMotorista.ExecutarAsync(id);
        return NoContent();
    }
}
