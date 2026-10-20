using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.ViagemRequisicaoDTO;
namespace API_Rodoviaria.Domain.Interfaces.IViagem;

public interface IAtualizarViagemCasoDeUso
{
    Task<RespostaCriarViagemDTO> ExecutarAsync(int id, RequisicaoAtualizarViagemDTO requisicao);
}
