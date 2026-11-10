using API_Rodoviaria.Domain.Exceptions;
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
            throw new ExcecaoDeNegocio("Não é possível deletar o motorista, pois ele possui viagens a serem feitas.",400);
        }

        var removeu = await _motoristaRepositorio.RemoverAsync(Id);
        if (!removeu)
        {
            throw new ExcecaoDeNegocio("Motorista não encontrado.");
        }
    }

}
