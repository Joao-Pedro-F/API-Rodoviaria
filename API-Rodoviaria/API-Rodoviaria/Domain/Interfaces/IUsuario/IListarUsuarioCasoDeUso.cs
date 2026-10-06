using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IUsuario
{
    public interface IListarUsuarioCasoDeUso
    {
        Task<RespostaCriarUsuarioDTO> ExecutarAsync(int id);
    }
}
