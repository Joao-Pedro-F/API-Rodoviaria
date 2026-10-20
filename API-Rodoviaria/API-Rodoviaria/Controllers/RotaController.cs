using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.RotaRequisicaoDTO;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces.IRota;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Rodoviaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = NomesPerfis.Admin)]
    public class RotaController : ControllerBase
    {
        private readonly ICriarRotaCasoDeUso _criarRota;
        private readonly IPaginacaoRotaCasoDeUso _listarRotas;
        private readonly IObterRotaPorId _obterRotaPorId;
        private readonly IAtualizarRotaCasoDeUso _atualizarRota;
        private readonly IDeletarRotaCasoDeUso _deletarRota;
        public RotaController(
        ICriarRotaCasoDeUso criarRota,
        IPaginacaoRotaCasoDeUso listarRotas,
        IObterRotaPorId obterRotaPorId,
        IAtualizarRotaCasoDeUso atualizarRota,
        IDeletarRotaCasoDeUso deletarRota)
        {
            _criarRota = criarRota;
            _listarRotas = listarRotas;
            _obterRotaPorId = obterRotaPorId;
            _atualizarRota = atualizarRota;
            _deletarRota = deletarRota;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] RequisicaoCriarRotaDTO request)
        {
            var resposta = await _criarRota.ExecutarAsync(request);
            return StatusCode(StatusCodes.Status201Created, resposta);
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] PaginacaoRequisicao paginacao)
        {
            return Ok(await _listarRotas.ExecutarAsync(paginacao));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            return Ok(await _obterRotaPorId.ExecutarAsync(id));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] RequisicaoAtualizarRotaDTO request)
        {
            return Ok(await _atualizarRota.ExecutarAsync(id, request));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _deletarRota.ExecutarAsync(id);
            return NoContent();
        }
    }
}
