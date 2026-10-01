using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.CriarUsuario;
using API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
namespace API_Rodoviaria.Controllers.Posts;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly ICriarUsuarioCasoDeUso _criarUsuario;
    public UsuariosController(ICriarUsuarioCasoDeUso criarUsuario)
    {
        _criarUsuario = criarUsuario;
    }
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] RequisicaoCriarUsuarioDTO requisicao)
    {
        var resposta = await _criarUsuario.ExecutarAsync(requisicao);
        return StatusCode(StatusCodes.Status201Created, resposta);
    }
}
