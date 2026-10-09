using API_Rodoviaria.Domain.Models;
namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;

public interface IRepositorioViagem
{
    Task AdicionarAsync(Viagem viagem);
    Task<Viagem?> ObterPorIdAsync(int Id);
    Task<(List<Viagem> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina);
    Task AtualizarAsync(Viagem viagem);
    Task<bool> RemoverAsync(int Id);
    Task<bool> TemReservasAsync(int Id);
    Task<bool> MotoristaTemViagemNoPeriodoAsync(int Id, DateTime saida, DateTime chegada, int? idViagemExcluida = null);
    Task<bool> OnibusTemViagemNoPeriodoAsync(int Id, DateTime saida, DateTime chegada, int? idViagemExcluida = null);
    //Task<List<Viagem>> ListarProximaAsync();
}
