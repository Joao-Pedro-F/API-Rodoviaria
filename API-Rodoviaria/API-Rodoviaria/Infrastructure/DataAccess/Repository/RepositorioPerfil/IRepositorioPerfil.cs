using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioPerfil
{
    public interface IRepositorioPerfil
    {
        Task CriarPerfil(Perfil perfil);
    }
}