using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioRota;

public class RepositorioRota
{
    private readonly RodoviariaDbContext _context;

    public RepositorioRota(RodoviariaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Rota rota)
    {
        _context.Rotas.Add(rota);
        await _context.SaveChangesAsync();
    }
    public async Task<Rota?> ObterPorIdAsync(int Id)
    {
        return await _context.Rotas.FirstOrDefaultAsync(r => r.Id == Id);
    }
    public async Task<(List<Rota> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina)
    {
        var query = _context.Rotas.AsNoTracking().OrderBy(r => r.Id);
        var total = await query.CountAsync();
        var itens = await query.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
        return (itens, total);
    }
    public async Task AtualizarAsync(Rota rota)
    {
        _context.Rotas.Update(rota);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> RemoverAsync(int Id)
    {
        var rota = await _context.Rotas.FindAsync(Id);
        if (rota is null) return false;

        _context.Rotas.Remove(rota);
        await _context.SaveChangesAsync(); return true;
    }
    public async Task<bool> TemViagensAsync(int Id)
    {
        return await _context.Viagens.AnyAsync(v => v.FkRota == Id);
    }
}
