namespace API_Rodoviaria.Domain.Interfaces.IViagem;

public interface IDeletarViagemCasoDeUso
{
    Task ExecutarAsync(int id);
}
