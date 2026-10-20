using API_Rodoviaria.Application.DTO_s.Requisicao.RotaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IRota
{
    public interface IAtualizarRotaCasoDeUso
    {
        Task<RespostaCriarRota> ExecutarAsync(int id, RequisicaoAtualizarRotaDTO requisicao);
    }
}
