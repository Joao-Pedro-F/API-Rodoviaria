using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IOnibus;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;

namespace API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.VerOnibus
{
    public class ListarOnibusCasoDeUso : IListarOnibusCasoDeUso
    {
        private readonly IRepositorioOnibus _onibusRepositorio;

        public ListarOnibusCasoDeUso(IRepositorioOnibus onibusRepositorio)
        {
            _onibusRepositorio = onibusRepositorio;
        }

        public async Task<PaginacaoResposta<RespostaCriarOnibusDTO>> ExecutarAsync(PaginacaoRequisicao paginacao)
        {
            var (itens, total) = await _onibusRepositorio.ListarPaginadoAsync(paginacao.Pagina, paginacao.TamanhoPagina);

            return new PaginacaoResposta<RespostaCriarOnibusDTO>
            {
                Itens = itens.Select(o => new RespostaCriarOnibusDTO
                {
                    Id = o.Id,
                    Placa = o.Placa,
                    CapacidadeTotal = o.CapacidadeTotal
                }).ToList(),
                PaginaAtual = paginacao.Pagina,
                TamanhoPagina = paginacao.TamanhoPagina,
                TotalItens = total,


            };

        }
    }
}
