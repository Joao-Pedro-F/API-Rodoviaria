using System.Runtime.Intrinsics.X86;
using API_Rodoviaria.Domain.Interfaces.IReserva;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva;

namespace API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.DeletarReserva
{
    public class DeletarReservaCasoDeUso : IDeletarReservaCasoDeUso
    {
        private readonly IRepositorioReserva _repositorioReserva;
        public DeletarReservaCasoDeUso(IRepositorioReserva repositorioReserva)
        {
            _repositorioReserva = repositorioReserva;
        }
        public async Task ExecutarAsync(int idUsuario, int idReserva)
        {
            var reserva = await _repositorioReserva.ObterPorIdAsync(idReserva) ?? throw new KeyNotFoundException("Reserva não encontrada.");
        if (reserva.FkUsuario != idUsuario)
                throw new UnauthorizedAccessException("Essa reserva nãopertence a esse usuário.");
                await _repositorioReserva.RemoverAsync(idReserva);
        }
    }
}
