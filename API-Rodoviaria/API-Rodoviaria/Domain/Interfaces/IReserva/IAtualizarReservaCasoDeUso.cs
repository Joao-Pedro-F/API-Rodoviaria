using API_Rodoviaria.Application.DTO_s.Requisicao.ReservaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ReservaRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IReserva
{
    public interface IAtualizarReservaCasoDeUso
    {
        Task<RespostaCriarReservaDTO> ExecutarAsync(int idUsuario, int idReserva, RequisicaoAtualizarReservaDTO request);
    }
}
