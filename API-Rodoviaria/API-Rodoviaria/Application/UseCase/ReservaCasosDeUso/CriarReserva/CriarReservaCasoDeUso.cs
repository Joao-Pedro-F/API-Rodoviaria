using API_Rodoviaria.Application.DTO_s.Requisicao.ReservaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ReservaRespostaDTO;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Domain.Interfaces.IReserva;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioCadeira;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;

namespace API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.CriarReserva
{
    public class CriarReservaCasoDeUso : ICriarReservaCasoDeUso
    {
        private readonly IRepositorioViagem _repositorioViagem;
        private readonly IRepositorioCadeira _repositorioCadeira;
        private readonly IRepositorioReserva _repositorioReserva;

        public CriarReservaCasoDeUso(
            IRepositorioViagem repositorioViagem,
            IRepositorioCadeira repositorioCadeira,
            IRepositorioReserva repositorioReserva)
        {
            _repositorioViagem = repositorioViagem;
            _repositorioCadeira = repositorioCadeira;
            _repositorioReserva= repositorioReserva;

        }

        public async Task<RespostaCriarReservaDTO> ExecutarAsync(int idUsuario, RequisicaoCriarReservaDTO requisicao)
        {
            var idsCadeiras= requisicao.IdsCadeiras.Distinct().ToList();

            var viagem = await _repositorioViagem.ObterPorIdAsync(requisicao.FkViagem) ?? throw new ExcecaoDeNegocio("Viagem não encontrada.", 404);

            if (viagem.DataSaida <= DateTime.UtcNow)
                throw new ExcecaoDeNegocio("Essa viagem já partiu.");

            var cadeiras = await _repositorioCadeira.ObterPorIdsAsync(idsCadeiras);
            if (cadeiras.Count != idsCadeiras.Count || cadeiras.Any(c => c.FkOnibus != viagem.FkOnibus))
                throw new ExcecaoDeNegocio("Uma ou mais cadeiras não pertencem ao ônibus dessa viagem.");

            var reserva = new Reserva
            {
                DataReserva= DateTime.UtcNow,
                FkUsuario=idUsuario,
                FkViagem=viagem.Id,
                ReservaCadeiras=idsCadeiras
                .Select(id=> new ReservaCadeira { FkCadeira=id})
                .ToList(),
            };

            var gravou = await _repositorioReserva.AdicionarSeCadeirasLivresAsync(reserva);
            if (!gravou)
                throw new ExcecaoDeNegocio("Uma ou mais cadeiras já foram reservadas nessa viagem.", 409);

            return new RespostaCriarReservaDTO
            {
                Id= reserva.Id,
                FkViagem=viagem.Id,
                DataReserva=reserva.DataReserva,
                NumerosCadeiras= cadeiras.Select(c=>c.Numero).OrderBy(n=>n).ToList()
            };
        }
    }
}
