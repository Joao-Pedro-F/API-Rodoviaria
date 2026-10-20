using API_Rodoviaria.Application.DTO_s.Resposta.ReservaRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IReserva;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva;

namespace API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.VerReservas
{
    public class ObterReservaPorId : IObterReservaPorId
    {
        private readonly IRepositorioReserva _repositorioReserva;
        public ObterReservaPorId(IRepositorioReserva repositorioReserva)
        {
            _repositorioReserva = repositorioReserva;
        }
        public async Task<RespostaCriarReservaDTO> ExecutarAsync(int idReserva)
        {
            var r = await _repositorioReserva.ObterPorIdAsync(idReserva)   ?? throw new KeyNotFoundException("Reserva não  encontrada.");
        return new RespostaCriarReservaDTO
        {
            Id = r.Id,
            FkViagem = r.FkViagem,
            FkUsuario = r.FkUsuario,
            DataReserva = r.DataReserva,
            NumerosCadeiras = r.ReservaCadeiras.Select(rc =>  rc.Cadeira.Numero).OrderBy(n => n).ToList()
        };
        }
    }
}
