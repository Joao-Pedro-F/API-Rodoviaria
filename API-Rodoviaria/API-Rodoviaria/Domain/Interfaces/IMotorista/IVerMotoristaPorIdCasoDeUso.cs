using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
namespace API_Rodoviaria.Domain.Interfaces.IMotorista;
public interface IVerMotoristaPorIdCasoDeUso
{
    Task<RespostaCriarMotoristaDTO> ExecutarAsync(int Id);
}
