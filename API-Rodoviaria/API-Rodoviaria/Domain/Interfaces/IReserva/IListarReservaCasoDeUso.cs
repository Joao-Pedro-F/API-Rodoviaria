using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ReservaRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IReserva
{
    public interface IListarReservaCasoDeUso
    {
        Task<PaginacaoResposta<RespostaCriarReservaDTO>>ExecutarAsync(PaginacaoRequisicao paginacao);
    }
}
