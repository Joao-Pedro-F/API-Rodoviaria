using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IOnibus;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;

namespace API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.VerOnibus
{
    public class VerOnibusPorIdCasoDeUso : IVerOnibusPorIdCasoDeUso
    {
        private readonly IRepositorioOnibus _onibusRepositorio;
        public VerOnibusPorIdCasoDeUso(IRepositorioOnibus onibusRepositorio)
        {
            _onibusRepositorio = onibusRepositorio;
        }
        public async Task<RespostaCriarOnibusDTO> ExecutarAsync(int Id)
        {
            var onibus = await _onibusRepositorio.ObterPorIdAsync(Id)?? throw new KeyNotFoundException($"Ônibus com ID {Id} não encontrado.");

            return new RespostaCriarOnibusDTO
            {
                Id = onibus.Id,
                Placa = onibus.Placa,
                CapacidadeTotal = onibus.CapacidadeTotal,
            };
        }
    }
}
