using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.ICadeira
{
    public interface IListarCadeirasDisponiveisCasoDeUso
    {
        Task<List<RespostaCadeiraViagemDTO>> ExecutarAsync(int idViagem);
    }
}
