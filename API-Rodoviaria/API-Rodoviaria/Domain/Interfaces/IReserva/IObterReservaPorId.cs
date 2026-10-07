using API_Rodoviaria.Application.DTO_s.Resposta.ReservaRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IReserva
{
    public interface IObterReservaPorId
    {
        Task<RespostaCriarReservaDTO> ExecutarAsync(int idReserva);
    }
}
