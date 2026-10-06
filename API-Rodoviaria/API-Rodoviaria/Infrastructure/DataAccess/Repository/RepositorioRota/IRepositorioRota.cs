using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioRota;

public interface IRepositorioRota
{
    Task AdicionarAsync(Rota rota);
    Task<Rota?> ObterPorIdAsync(int Id);
    Task<(List<Rota> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina);
    Task AtualizarAsync(Rota rota);
    Task<bool> RemoverAsync(int Id);
    Task<bool> TemViagensAsync(int Id);
}
