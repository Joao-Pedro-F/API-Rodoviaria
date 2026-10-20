using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;
namespace API_Rodoviaria.Domain.Interfaces.IViagem
{
    public interface IListarViagensCasoDeUso
    {
        Task<RespostaCriarViagemDTO> ExecutarAsync(int id);

    }
}
