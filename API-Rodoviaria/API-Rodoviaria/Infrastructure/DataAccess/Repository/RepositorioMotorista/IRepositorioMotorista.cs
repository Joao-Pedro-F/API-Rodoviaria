using API_Rodoviaria.Domain.Models;
namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;

public interface IRepositorioMotorista
{
    Task AdicionarAsync(Motorista motorista);
    Task<Motorista?> ObterPorIdAsync(int Id);
    Task<bool> ExisteAsync(string Cpf, string Cnh);
}
