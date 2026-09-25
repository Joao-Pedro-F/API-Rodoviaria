namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;

using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;

public class RepositorioOnibus : IRepositorioOnibus
{
     private readonly RodoviariaDbContext _context;

    public RepositorioOnibus(RodoviariaDbContext context)
        {
        _context = context;
    }

    public Task AdicionarAsync(Perfil perfil)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistePlacaAsync(string placa)
    {
        return await _context.Onibus.AnyAsync(o => o.Placa == placa);
    }

    public Task<Perfil> ObterPorNomeAsync(string nomeCargo)
    {
        throw new NotImplementedException();
    }
}
