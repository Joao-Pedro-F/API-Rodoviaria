namespace API_Rodoviaria.Domain.Interfaces.IMotorista;

public interface IDeletarMotoristaCasoDeUso
{
    Task ExecutarAsync(int Id);
}
