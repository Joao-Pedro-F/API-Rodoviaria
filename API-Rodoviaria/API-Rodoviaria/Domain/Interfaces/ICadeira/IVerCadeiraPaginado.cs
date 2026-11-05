using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.CadeiraRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.ICadeira
{
    public interface IVerCadeiraPaginado
    {
        Task<PaginacaoResposta<RespostaCriarCadeiraDTO>> ExecutarAsync(PaginacaoRequisicao paginacao);
    }
}
