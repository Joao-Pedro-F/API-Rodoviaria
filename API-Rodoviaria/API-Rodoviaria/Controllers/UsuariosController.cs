using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.VerUsuario;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces.IUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
namespace API_Rodoviaria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    public const string NomeCookie = "access_token";
    private readonly ICriarUsuarioCasoDeUso _criarUsuario;
    private readonly ILoginCasoDeUso _login;
    private readonly IPaginacaoUsuarioCasoDeUso _listarUsuarios;
    private readonly IListarUsuarioCasoDeUso _obterUsuarioPorId;
    private readonly IAtualizarUsuarioCasoDeUso _atualizarUsuario;
    private readonly IDeletarUsuarioCasoDeUso _deletarUsuario;

    public UsuariosController(
        ICriarUsuarioCasoDeUso criarUsuario,
        ILoginCasoDeUso login,
        IPaginacaoUsuarioCasoDeUso listarUsuarios,
        IListarUsuarioCasoDeUso obterUsuarioPorId,
        IAtualizarUsuarioCasoDeUso atualizarUsuario,
        IDeletarUsuarioCasoDeUso deletarUsuario)
    {
        _criarUsuario = criarUsuario;
        _login = login;
        _listarUsuarios = listarUsuarios;
        _obterUsuarioPorId = obterUsuarioPorId;
        _atualizarUsuario = atualizarUsuario;
        _deletarUsuario = deletarUsuario;
    }

    [HttpPost]
    [AllowAnonymous] 
    public async Task<IActionResult> Criar([FromBody]RequisicaoCriarUsuarioDTO request)
    {
        var resposta = await _criarUsuario.ExecutarAsync(request);
        return StatusCode(StatusCodes.Status201Created, resposta);
    }
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] RequisicaoLoginDTO request)
    {
        var resposta = await _login.ExecutarAsync(request);
        Response.Cookies.Append(NomeCookie, resposta.Token, new
        CookieOptions
        {
            HttpOnly = true,
            Secure = Request.IsHttps,
            SameSite = SameSiteMode.Strict,
            Expires = new DateTimeOffset(resposta.ExpiraEm,
        TimeSpan.Zero)
        });
        return Ok(resposta);
    }
    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(NomeCookie);
        return NoContent();
    }

    // GET /api/usuario?pagina=1&tamanhoPagina=10
    [HttpGet]
    [Authorize(Roles = NomesPerfis.Admin)]
    public async Task<IActionResult> Listar([FromQuery]PaginacaoRequisicao paginacao)
    {
        return Ok(await _listarUsuarios.ExecutarAsync(paginacao));
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = NomesPerfis.Admin)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return Ok(await _obterUsuarioPorId.ExecutarAsync(id));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = NomesPerfis.Admin)]
    public async Task<IActionResult> Atualizar(int id, [FromBody]RequisicaoAtualizarUsuarioDTO request)
    {
        return Ok(await _atualizarUsuario.ExecutarAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = NomesPerfis.Admin)]
    public async Task<IActionResult> Deletar(int id)
    {
        await _deletarUsuario.ExecutarAsync(id);
        return NoContent();
    }

}
