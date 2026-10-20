using API_Rodoviaria.Domain.Interfaces.IUsuario;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario;

namespace API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.DeletarUsuario
{
    public class DeletarUsuarioCasoDeUso : IDeletarUsuarioCasoDeUso
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        
        public DeletarUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public async Task ExecutarAsync(int id)
        {
            if (await _repositorioUsuario.TemReservasAsync(id))
                throw new InvalidOperationException("Esse usuário tem reservas e não pode ser removido");
            
            var removeu= await _repositorioUsuario.RemoverAsync(id);
            if (!removeu)
                throw new KeyNotFoundException("Usuário não encontrado.");
        }
    }

}
