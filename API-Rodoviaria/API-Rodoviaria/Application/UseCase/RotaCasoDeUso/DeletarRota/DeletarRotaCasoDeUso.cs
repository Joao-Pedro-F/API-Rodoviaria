using API_Rodoviaria.Domain.Interfaces.IRota;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioRota;

namespace API_Rodoviaria.Application.UseCase.RotaCasoDeUso.DeletarRota
{
    public class DeletarRotaCasoDeUso : IDeletarRotaCasoDeUso
    {
        private readonly RepositorioRota _repositorioRota;

        public DeletarRotaCasoDeUso(RepositorioRota repositorioRota)
        {
            _repositorioRota = repositorioRota;
        }

        public async Task ExecutarAsync(int id)
        {
            if (await _repositorioRota.TemViagensAsync(id)) throw new InvalidOperationException("Essa rota tem viagens e não pode ser removida.");

            var removeu = await _repositorioRota.RemoverAsync(id);
            if (!removeu) throw new KeyNotFoundException("Rota não encontrada");
        }
    }
}
