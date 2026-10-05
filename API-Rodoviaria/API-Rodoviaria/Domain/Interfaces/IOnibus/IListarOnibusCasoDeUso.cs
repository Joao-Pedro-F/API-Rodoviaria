using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IOnibus
{
    public interface IListarOnibusCasoDeUso
    {
        Task<PaginacaoResposta<RespostaCriarOnibusDTO>> ExecutarAsync(PaginacaoRequisicao paginacao);
    }
}
