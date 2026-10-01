using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;

namespace API_Rodoviaria.Application.UseCase.CadeiraCasosDeUso.ListarCadeira
{
    public interface IListarCadeirasDisponiveisCasoDeUso
    {
        Task<List<RespostaCadeiraViagemDTO>> ExecutarAsync(int idViagem);
    }
}
