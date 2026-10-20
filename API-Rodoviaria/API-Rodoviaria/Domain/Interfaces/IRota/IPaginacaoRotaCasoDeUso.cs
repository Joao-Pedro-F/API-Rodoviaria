using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IRota
{
    public interface IPaginacaoRotaCasoDeUso
    {
        Task<PaginacaoResposta<RespostaCriarRota>> ExecutarAsync(PaginacaoRequisicao paginacao);
    }
}
