using API_Rodoviaria.Application.DTO_s.Requisicao.ReservaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ReservaRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IReserva
{
    public interface ICriarReservaCasoDeUso
    {
        Task<RespostaCriarReservaDTO> ExecutarAsync(int id, RequisicaoCriarReservaDTO requisicao);
    }
}
