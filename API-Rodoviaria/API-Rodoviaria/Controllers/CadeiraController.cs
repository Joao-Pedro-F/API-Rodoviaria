using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.UseCase.CadeiraCasosDeUso.ListarCadeira;
using API_Rodoviaria.Application.UseCase.CadeiraCasosDeUso.VerCadeira;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces.ICadeira;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Rodoviaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadeiraController : ControllerBase
    {
        public readonly IVerCadeiraPaginado _verCadeiraPaginado;
        public readonly IListarCadeirasDisponiveisCasoDeUso _listarCadeirasDisponiveis;

        public CadeiraController(IVerCadeiraPaginado verCadeiraPaginado, IListarCadeirasDisponiveisCasoDeUso listarCadeirasDisponiveis)
        {
            _verCadeiraPaginado = verCadeiraPaginado;
            _listarCadeirasDisponiveis = listarCadeirasDisponiveis;
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] PaginacaoRequisicao paginacao)
        {
            return Ok(await _verCadeiraPaginado.ExecutarAsync(paginacao));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ListarCadeirasDisponiveis(int id)
        {
            return Ok(await _listarCadeirasDisponiveis.ExecutarAsync(id));
        }

    }
}
