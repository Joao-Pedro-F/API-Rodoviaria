using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Requisicao.MotoristaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
namespace API_Rodoviaria.Domain.Interfaces.IMotorista;

public interface ICriarMotoristaCasoDeUso
{
    Task<RespostaCriarMotoristaDTO> ExecutarAsync(RequisicaoCriarMotoristaDTO requisicao);
}
