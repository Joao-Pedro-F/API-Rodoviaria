using System.Runtime.Intrinsics.X86;
using API_Rodoviaria.Application.DTO_s.Requisicao.ReservaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ReservaRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IReserva;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva;

namespace API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.AtualizarReserva
{
    public class AtualizarReservaCasoDeUso : IAtualizarReservaCasoDeUso
    {

        private readonly IRepositorioReserva _repositorioReserva;
        private readonly IRepositorioOnibus _repositorioOnibus;
        public AtualizarReservaCasoDeUso(IRepositorioReserva repositorioReserva, IRepositorioOnibus repositorioOnibus)
        {
            _repositorioReserva = repositorioReserva;
            _repositorioOnibus = repositorioOnibus;
        }
        public async Task<RespostaCriarReservaDTO> ExecutarAsync(int idUsuario, int
        idReserva, RequisicaoAtualizarReservaDTO request)
        {
            var reserva = await _repositorioReserva.ObterPorIdAsync(idReserva)?? throw new KeyNotFoundException("Reserva não encontrada.");
           
        if (reserva.FkUsuario != idUsuario)
                throw new UnauthorizedAccessException("Essa reserva não  pertence a esse usuário.");

                var numeros = request.IdsCadeiras.Distinct().ToList();
                var onibusDaViagem = reserva.Viagem.FkOnibus;
                var cadeiras = await _repositorioOnibus.ObterCadeirasPorNumeroAsync(onibusDaViagem,numeros);
            if (cadeiras.Count != numeros.Count)
                throw new ArgumentException("Uma ou mais cadeiras não existem nesse ônibus.");
                var idsCadeiras = cadeiras.Select(c => c.Id).ToList();
            if (!await _repositorioReserva.AtualizarCadeirasSeLivresAsync(idReserva,  idsCadeiras))
                throw new InvalidOperationException("Uma ou mais cadeiras já foram reservadas nessa viagem.");
                var atualizada = await  _repositorioReserva.ObterPorIdAsync(idReserva);
            return new RespostaCriarReservaDTO
            {
                Id = atualizada!.Id,
                FkViagem = atualizada.FkViagem,
                FkUsuario= atualizada.FkUsuario,
                DataReserva = atualizada.DataReserva,
                NumerosCadeiras = atualizada.ReservaCadeiras.Select(rc =>  rc.Cadeira.Numero).OrderBy(n => n).ToList()
            };
        }

    }
}
