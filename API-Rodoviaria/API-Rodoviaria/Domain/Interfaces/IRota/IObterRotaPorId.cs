using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IRota
{
    public interface IObterRotaPorId
    {
        Task<RespostaCriarRota> ExecutarAsync(int id);

    }
}
