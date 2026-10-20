using API_Rodoviaria.Application.UseCase.PerfilCasosDeUso.CriarPerfil;

namespace API_Rodoviaria.Domain.Interfaces.IPerfil
{
    public interface  ICriarPerfilCasoDeUso
    {
        Task<RespostaCriarPerfilDTO> ExecutarAsync(RequisicaoCriarPerfilDTO requisicao);
    }
}