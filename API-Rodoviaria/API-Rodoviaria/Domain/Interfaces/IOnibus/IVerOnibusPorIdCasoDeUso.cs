using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;

namespace API_Rodoviaria.Domain.Interfaces.IOnibus
{
    public interface IVerOnibusPorIdCasoDeUso
    {
        Task<RespostaCriarOnibusDTO> ExecutarAsync(int Id);
    }
}
