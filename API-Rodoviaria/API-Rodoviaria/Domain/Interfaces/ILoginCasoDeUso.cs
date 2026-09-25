using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using System.Threading.Tasks;

namespace API_Rodoviaria.Domain.Interfaces
{
    public interface ILoginCasoDeUso
    {
        Task<RespostaLoginDTO> ExecutarAsync(RequisicaoLoginDTO requisicao);
    }
}
