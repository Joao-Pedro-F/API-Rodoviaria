using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario
{
    public interface IRepositorioUsuario
    {
        Task AdicionarAsync(Usuario usuario);
        Task<Usuario?> ObterPorIdAsync(int id);
        Task<Usuario?> ObterPorUsernameAsync(string username);
        Task<bool> ExisteAsync(string username,string email, string cpf);
        Task<Perfil?> ObterPerfilPorNomeAsync(string cargo);
        Task<(List<Usuario> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina);
        Task AtualizarAsync(Usuario usuario);
        Task<bool> RemoverAsync(int id);
        Task<bool> TemReservasAsync(int id);

    }
}
