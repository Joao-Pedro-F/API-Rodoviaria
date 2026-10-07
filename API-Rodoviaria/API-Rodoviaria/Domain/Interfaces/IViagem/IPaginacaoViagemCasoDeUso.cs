using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IViagem
{
    public interface IPaginacaoViagemCasoDeUso
    {
        Task<PaginacaoResposta<RespostaCriarViagemDTO>> ExecutarAsync(PaginacaoRequisicao requisicao);
    }
}
