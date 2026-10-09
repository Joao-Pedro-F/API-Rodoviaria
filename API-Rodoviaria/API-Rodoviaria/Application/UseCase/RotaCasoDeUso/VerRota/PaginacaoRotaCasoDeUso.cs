using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IRota;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioRota;

namespace API_Rodoviaria.Application.UseCase.RotaCasoDeUso.VerRota
{
    public class PaginacaoRotaCasoDeUso : IPaginacaoRotaCasoDeUso
    {
        private readonly IRepositorioRota _repositorioRota;

        public PaginacaoRotaCasoDeUso(IRepositorioRota repositorioRota)
        {
            _repositorioRota = repositorioRota;
        }
        public async Task<PaginacaoResposta<RespostaCriarRota>> ExecutarAsync(PaginacaoRequisicao paginacao)
        {
            var (itens, total) = await _repositorioRota.ListarPaginadoAsync(paginacao.Pagina, paginacao.TamanhoPagina);
            return new PaginacaoResposta<RespostaCriarRota>
            {
                Itens = itens.Select(r => new RespostaCriarRota
                {
                    Id = r.Id,
                    EnderecoInicio = r.EnderecoInicio,
                    EnderecoFim = r.EnderecoFim,
                }).ToList(),
                PaginaAtual = paginacao.Pagina,
                TamanhoPagina = paginacao.TamanhoPagina,
                TotalItens = total
            };



        }
    }
}