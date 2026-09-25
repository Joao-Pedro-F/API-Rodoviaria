using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;

public interface IRepositorioOnibus
{
    Task<bool> ExistePlacaAsync(string placa);
    Task AdicionarAsync(Perfil perfil);
    Task<Perfil> ObterPorNomeAsync(string nomeCargo);

}
