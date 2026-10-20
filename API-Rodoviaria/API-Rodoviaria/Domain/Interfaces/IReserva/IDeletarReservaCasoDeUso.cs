namespace API_Rodoviaria.Domain.Interfaces.IReserva
{
    public interface IDeletarReservaCasoDeUso
    {
        Task ExecutarAsync(int idUsuario, int idReserva);
    }
}
