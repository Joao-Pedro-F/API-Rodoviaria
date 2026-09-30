using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva
{
    public interface IRepositorioReserva
    {
        Task<List<int>> ObterIdsCadeirasReservadasAsync(int idViagem);
        Task<bool> AdicionarSeCadeirasLivresAsync(Reserva reserva);
    
    }
}
