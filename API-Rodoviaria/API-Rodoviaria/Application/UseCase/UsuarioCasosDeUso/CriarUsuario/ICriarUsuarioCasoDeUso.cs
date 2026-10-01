using API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;

namespace API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.CriarUsuario
{
    public interface ICriarUsuarioCasoDeUso
    {
        Task<RespostaCriarUsuarioDTO> ExecutarAsync(RequisicaoCriarUsuarioDTO requisicao);
    }
}
