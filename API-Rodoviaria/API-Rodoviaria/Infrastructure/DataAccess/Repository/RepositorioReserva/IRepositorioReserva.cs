using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva
{
    public interface IRepositorioReserva
    {
        Task<Reserva?> ObterPorIdAsync(int idReserva);
        Task<(List<Reserva> Itens, int Total)> ListarPaginadoAsync(int
        pagina, int tamanhoPagina);
        Task<bool> AdicionarSeCadeirasLivresAsync(Reserva reserva);
        Task<bool> AtualizarCadeirasSeLivresAsync(int idReserva, List<int>
        novosIdsCadeiras);
        Task<bool> RemoverAsync(int idReserva);
        Task <List<int>> ObterIdsCadeirasReservadasAsync(int idViagem);

    }
}
