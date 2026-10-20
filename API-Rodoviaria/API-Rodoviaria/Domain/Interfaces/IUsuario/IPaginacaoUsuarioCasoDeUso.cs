using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IUsuario
{
    public interface IPaginacaoUsuarioCasoDeUso
    {
        Task<PaginacaoResposta<RespostaCriarUsuarioDTO>> ExecutarAsync(PaginacaoRequisicao paginacao);
    }
}
