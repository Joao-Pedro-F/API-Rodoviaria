using API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IUsuario
{
    public interface IAtualizarUsuarioCasoDeUso
    {
        Task<RespostaCriarUsuarioDTO> ExecutarAsync(int id, RequisicaoAtualizarUsuarioDTO requisicao);
    }
}
