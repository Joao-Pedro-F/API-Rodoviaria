using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
namespace API_Rodoviaria.Domain.Interfaces.IMotorista;

public interface IVerMotoristaPaginadoCasoDeUso
 {
        Task<PaginacaoResposta<RespostaCriarMotoristaDTO>> ExecutarAsync(PaginacaoRequisicao paginacao);
}

