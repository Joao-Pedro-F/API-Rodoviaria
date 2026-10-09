using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Domain.Interfaces.IMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
namespace API_Rodoviaria.Application.UseCase.MotoristaCasosDeUso.DeletarMotorista;

public class DeletarMotoristaCasoDeUso : IDeletarMotoristaCasoDeUso
{
    private readonly IRepositorioMotorista _motoristaRepositorio;

    public DeletarMotoristaCasoDeUso(IRepositorioMotorista motoristaRepositorio)
    {
        _motoristaRepositorio = motoristaRepositorio;
    }

    public async Task ExecutarAsync(int Id)
    {
        if(await _motoristaRepositorio.TemViagensAsync(Id))
        {
            throw new InvalidOperationException("Não é possível deletar o motorista, pois ele possui viagens a serem feitas.");
        }

        var removeu = await _motoristaRepositorio.RemoverAsync(Id);
        if (!removeu)
        {
            throw new KeyNotFoundException("Motorista não encontrado.");
        }
    }

}
