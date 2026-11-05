using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.CadeiraRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.ICadeira;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioCadeira;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;

namespace API_Rodoviaria.Application.UseCase.CadeiraCasosDeUso.VerCadeira
{
    public class VerCadeiraPaginado : IVerCadeiraPaginado
    {

       
        private readonly IRepositorioCadeira _repositorioCadeira;
   

        public VerCadeiraPaginado(
            
            IRepositorioCadeira repositorioCadeira
            )
        {
           
            _repositorioCadeira = repositorioCadeira;
          
        }

        public async Task<PaginacaoResposta<RespostaCriarCadeiraDTO>> ExecutarAsync(PaginacaoRequisicao paginacao)
        {
            var (itens, total) = await _repositorioCadeira.ListarPaginadoAsync(paginacao.Pagina, paginacao.TamanhoPagina);
            return new PaginacaoResposta<RespostaCriarCadeiraDTO>
            {
                Itens = itens.Select(m => new RespostaCriarCadeiraDTO
                {
                    Id = m.Id,
                    Numero = m.Numero,
                    Disponivel = m.Disponivel,
                    FkOnibus = m.FkOnibus
                }).ToList(),
                PaginaAtual = paginacao.Pagina,
                TamanhoPagina = paginacao.TamanhoPagina,
                TotalItens = total,
            };
        }
    }
}
