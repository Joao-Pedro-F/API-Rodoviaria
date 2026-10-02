using API_Rodoviaria.Domain.Models;
namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista
{

public interface IRepositorioMotorista
{
    Task AdicionarAsync(Motorista motorista);
    Task<Motorista?> ObterPorIdAsync(int Id);
    Task<bool> ExisteAsync(string Cpf, string Cnh, int? Id);
    Task<(List<Motorista> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina);
    Task AtualizarAsync(Motorista motorista);
    Task<bool>RemoverAsync(int Id);
    Task<bool>TemViagensAsync(int Id);


    }
}