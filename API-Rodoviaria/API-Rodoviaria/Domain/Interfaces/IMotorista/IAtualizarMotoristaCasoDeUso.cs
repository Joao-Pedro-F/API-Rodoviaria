using API_Rodoviaria.Application.DTO_s.Requisicao.MotoristaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
namespace API_Rodoviaria.Domain.Interfaces.IMotorista;

public interface IAtualizarMotoristaCasoDeUso
{
    Task<RequisicaoCriarMotoristaDTO> Executar(int Id, RequisicaoAtualizarMotoristaDTO requisicao);
}
