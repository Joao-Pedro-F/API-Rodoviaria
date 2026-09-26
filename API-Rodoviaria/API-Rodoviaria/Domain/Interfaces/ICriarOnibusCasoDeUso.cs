using API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Resposta;
namespace API_Rodoviaria.Domain.Interfaces;

public interface ICriarOnibusCasoDeUso
{
    Task<RespostaCriarOnibusDTO> ExecutarAsync(RequisicaoCriarOnibusDTO
    requisicao);
}
