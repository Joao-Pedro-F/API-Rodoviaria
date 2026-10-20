using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.ViagemRequisicaoDTO;
namespace API_Rodoviaria.Domain.Interfaces.IViagem;

public interface ICriarViagemCasoDeUso
{
    Task<RespostaCriarViagemDTO> ExecutarAsync(RequisicaoCriarViagemDTO requisicao);
}
