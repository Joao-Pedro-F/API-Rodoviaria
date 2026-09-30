using API_Rodoviaria.Domain.Models;
namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;

public interface IRepositorioViagem
{
    Task AdicionarAsync(Viagem viagem);
    Task<Viagem?> ObterPorIdAsync(int Id);
    Task<bool> MotoristaTemViagemNoPeriodoAsync(int Id, DateTime saida, DateTime chegada);
    Task<List<Viagem>> ListarProximaAsync();
}
