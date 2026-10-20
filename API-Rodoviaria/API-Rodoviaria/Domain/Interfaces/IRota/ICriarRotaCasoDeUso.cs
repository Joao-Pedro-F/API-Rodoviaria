using API_Rodoviaria.Application.DTO_s.Requisicao.RotaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IRota;

public interface ICriarRotaCasoDeUso
{
    Task<RespostaCriarRota> ExecutarAsync(RequisicaoCriarRotaDTO requisicao);
}
