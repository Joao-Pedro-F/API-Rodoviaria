using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IRota;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioRota;

namespace API_Rodoviaria.Application.UseCase.RotaCasoDeUso.VerRota
{
    public class ObterRotaPorId : IObterRotaPorId
    {
        private readonly IRepositorioRota _repositorioRota;

        public ObterRotaPorId(IRepositorioRota repositorioRota)
        {
            _repositorioRota = repositorioRota;

        }

        public async Task<RespostaCriarRota> ExecutarAsync(int id)
        {
            var rota = await _repositorioRota.ObterPorIdAsync(id)?? throw new KeyNotFoundException("Rota não encontrada.");

            return new RespostaCriarRota
            {
                Id = rota.Id,
                EnderecoInicio = rota.EnderecoInicio,
                EnderecoFim = rota.EnderecoFim
            };
        }
    }
}
