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

    public async Task<bool> ExistePlacaAsync(string placa, int? idExcluir=null)
    {
        return await _context.Onibus.AnyAsync(o => o.Placa == placa && (idExcluir ==null || o.Id != idExcluir));
    }

    public Task<Perfil> ObterPorNomeAsync(string nomeCargo)
    {
        throw new NotImplementedException();
    }
    public async Task<Onibus?> ObterPorIdAsync(int id)
    {
        return await _context.Onibus.AsNoTracking()
            .Include(o => o.Cadeiras)
            .FirstOrDefaultAsync(o=> o.Id == id);
    }

    public async Task<List<Cadeira>> ObterCadeirasPorNumeroAsync(int id, IEnumerable<int> numeros)
    {
        var lista= numeros.ToList();
        return await _context.Cadeiras.AsNoTracking()
            .Where(c => c.FkOnibus == id && lista.Contains(c.Numero))
            .ToListAsync();
    }

    public async Task<(List<Onibus> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina)
    {
        var query= _context.Onibus.AsNoTracking().OrderBy(o=> o.Id);
        var total = await query.CountAsync();
        var itens = await query.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
                return (itens, total);
    }

    public async Task AtualizarAsync(Onibus onibus)
    {
        _context.Onibus.Attach(onibus);
        _context.Entry(onibus).Property(o=> o.Placa).IsModified= true;
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> RemoverAsync(int id)
    {
        var onibus = await _context.Onibus.FindAsync(id);
        if (onibus == null)
        {
            return false;
        }
        _context.Onibus.Remove(onibus);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TemViagensAsync(int id)
    {
        return await _context.Viagens.AnyAsync(v => v.FkOnibus == id);
    }
    
   




}
