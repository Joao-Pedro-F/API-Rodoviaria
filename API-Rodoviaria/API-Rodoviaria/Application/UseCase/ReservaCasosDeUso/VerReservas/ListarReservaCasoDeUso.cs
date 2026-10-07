using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ReservaRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IReserva;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva;

namespace API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.VerReservas
{
    public class ListarReservaCasoDeUso : IListarReservaCasoDeUso
    {
        private readonly IRepositorioReserva _repositorioReserva;
        public ListarReservaCasoDeUso(IRepositorioReserva repositorioReserva)
        {
            _repositorioReserva = repositorioReserva;
        }
        public async Task<PaginacaoResposta<RespostaCriarReservaDTO>> ExecutarAsync(PaginacaoRequisicao paginacao)
        {
            var (itens, total) = await _repositorioReserva.ListarPaginadoAsync(paginacao.Pagina, paginacao.TamanhoPagina);
            return new PaginacaoResposta<RespostaCriarReservaDTO>
            {
                Itens = itens.Select(r => new RespostaCriarReservaDTO
                {
                    Id = r.Id,
                    FkViagem = r.FkViagem,
                    FkUsuario = r.FkUsuario,
                    DataReserva = r.DataReserva,
                    NumerosCadeiras = r.ReservaCadeiras.Select(rc =>
                    rc.Cadeira.Numero).OrderBy(n => n).ToList()
                }).ToList(),
                PaginaAtual = paginacao.Pagina,
                TamanhoPagina = paginacao.TamanhoPagina,
                TotalItens = total
            };
        }
    }
}
