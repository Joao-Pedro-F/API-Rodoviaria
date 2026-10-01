using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioCadeira;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;

namespace API_Rodoviaria.Application.UseCase.CadeiraCasosDeUso.ListarCadeira
{
    public class ListarCadeirasDisponiveisCasoDeUso :
IListarCadeirasDisponiveisCasoDeUso
    {
        private readonly IRepositorioViagem _repositorioViagem;
        private readonly IRepositorioCadeira _repositorioCadeira;
        private readonly IRepositorioReserva _repositorioReserva;
        public ListarCadeirasDisponiveisCasoDeUso(
        IRepositorioViagem repositorioViagem,
        IRepositorioCadeira repositorioCadeira,
        IRepositorioReserva repositorioReserva)
        {
            _repositorioViagem = repositorioViagem;
            _repositorioCadeira = repositorioCadeira;
            _repositorioReserva = repositorioReserva;
        }
        public async Task<List<RespostaCadeiraViagemDTO>> ExecutarAsync(int
        idViagem)
        {
            var viagem = await _repositorioViagem.ObterPorIdAsync(idViagem)
            ?? throw new ExcecaoDeNegocio("Viagem não encontrada.", 404);
            var cadeiras = await
            _repositorioCadeira.ObterPorOnibusAsync(viagem.FkOnibus);
            var ocupadas = (await
            _repositorioReserva.ObterIdsCadeirasReservadasAsync(idViagem)).ToHashSet();
            return cadeiras.Select(c => new RespostaCadeiraViagemDTO
            {
                Id = c.Id,
                Numero = c.Numero,
                Disponivel = !ocupadas.Contains(c.Id)
            }).ToList();
        }
    }
}
