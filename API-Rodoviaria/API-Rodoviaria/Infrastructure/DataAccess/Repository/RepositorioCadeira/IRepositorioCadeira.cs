using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioCadeira
{
    public interface IRepositorioCadeira
    {
        Task<List<Cadeira>> ObterPorOnibusAsync(int idOnibus);
        Task<List<Cadeira>> ObterPorIdsAsync(IEnumerable<int> idsCadeiras);

        Task<(List<Cadeira> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina);
    }
}
