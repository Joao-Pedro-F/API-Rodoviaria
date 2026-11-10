using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
namespace API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.DeletarViagem;

public class DeletarViagemCasoDeUso : IDeletarViagemCasoDeUso
{
    private readonly IRepositorioViagem _viagemRepositorio;

    public DeletarViagemCasoDeUso(IRepositorioViagem viagemRepositorio)
    {
        _viagemRepositorio = viagemRepositorio;
    }

    public async Task ExecutarAsync(int id)
    {
        if (await _viagemRepositorio.TemReservasAsync(id))
        {
            throw new ExcecaoDeNegocio("A viagem possui reservas e não pode ser deletada.", 422);
        }

        var removeu = await _viagemRepositorio.RemoverAsync(id);
        if(!removeu)
        {
            throw new ExcecaoDeNegocio("Não foi possível deletar a viagem.");
        }
    }
}
