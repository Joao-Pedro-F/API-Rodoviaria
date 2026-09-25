using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario
{
    public interface IRepositorioUsuario
    {
        Task AdicionarAsync(Usuario usuario);
        Task<Usuario?> ObterPorUsernameAsync(string username);
        Task<bool> ExisteAsync(string username,string email, string cpf);
    }
}
