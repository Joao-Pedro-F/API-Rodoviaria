
using API_Rodoviaria.Domain.Interfaces.IOnibus;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;

namespace API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.DeletarOnibus
{
    public class DeletarOnibusCasoDeUso : IDeletarOnibusCasoDeUso
    {
        private readonly IRepositorioOnibus _onibusRepositorio;

        public DeletarOnibusCasoDeUso(IRepositorioOnibus onibusRepositorio)
        {
            _onibusRepositorio = onibusRepositorio;
        }

        public async Task ExecutarAsync(int Id)
        {
            if(await _onibusRepositorio.TemViagensAsync(Id))
                throw new InvalidOperationException("Não é possível deletar o ônibus, pois ele possui viagens a serem feitas.");

            var removeu = await _onibusRepositorio.RemoverAsync(Id);
            if(!removeu)
                throw new KeyNotFoundException("Ônibus não encontrado.");
        }
    }
}
