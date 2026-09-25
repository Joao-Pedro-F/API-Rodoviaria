using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioPerfil;

namespace API_Rodoviaria.Application.UseCase.PerfilCasosDeUso.CriarPerfil
{
    public class CriarPerfilCasoDeUso(IRepositorioPerfil repositorio) : ICriarPerfilCasoDeUso
    {
        private readonly IRepositorioPerfil _repositorio = repositorio;

        public async Task Executar()
         {
            Perfil perfil = new Perfil
            {
                Id = 1,
                Cargo = "Administrador"
            };

            await _repositorio.CriarPerfil(perfil);
        }
    }
}
