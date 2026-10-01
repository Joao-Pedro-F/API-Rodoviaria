using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Application.UseCase.CadeiraCasosDeUso.ListarCadeira;
namespace API_Rodoviaria.Controllers.Gets;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ViagensController : ControllerBase
{
    private readonly IListarViagensCasoDeUso _listarViagens;
    private readonly IListarCadeirasDisponiveisCasoDeUso _listarCadeirasDisponiveis;

    public ViagensController(IListarViagensCasoDeUso listarViagens, IListarCadeirasDisponiveisCasoDeUso listarCadeirasDisponiveis)
    {
        _listarViagens = listarViagens;
        _listarCadeirasDisponiveis = listarCadeirasDisponiveis;
    }
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        return Ok(await _listarViagens.ExecutarAsync());
    }
    [HttpGet("{Id:int}/cadeiras")]
    public async Task<IActionResult> ListarCadeirasDisponiveis(int Id)
    {
        return Ok(await _listarCadeirasDisponiveis.ExecutarAsync(Id));
    }
}
