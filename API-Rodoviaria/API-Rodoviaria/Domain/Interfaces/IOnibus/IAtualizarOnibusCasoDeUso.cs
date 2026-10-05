using API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IOnibus
{
    public interface IAtualizarOnibusCasoDeUso
    {
        Task<RespostaCriarOnibusDTO> ExecutarAsync(int id, RequisicaoCriarOnibusDTO requisicao);
    }
}
