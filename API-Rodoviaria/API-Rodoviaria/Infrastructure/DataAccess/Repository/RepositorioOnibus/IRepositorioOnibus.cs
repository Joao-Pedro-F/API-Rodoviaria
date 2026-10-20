using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;

public interface IRepositorioOnibus
{
    Task<bool> ExistePlacaAsync(string placa, int? idExcluir = null);
    Task AdicionarAsync(Perfil perfil);
    Task<Perfil> ObterPorNomeAsync(string nomeCargo);
    Task<Onibus?> ObterPorIdAsync(int id);
    Task<List<Cadeira>> ObterCadeirasPorNumeroAsync(int id, IEnumerable<int> numeros);
    Task<(List<Onibus> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina);
    Task AtualizarAsync(Onibus onibus);
    Task<bool> RemoverAsync(int id);
    Task<bool> TemViagensAsync(int id);

}
