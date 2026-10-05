using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;
using API_Rodoviaria.Domain.Interfaces.IViagem;
namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
public class RepositorioViagem : IRepositorioViagem
{
    private readonly RodoviariaDbContext _context;

    public RepositorioViagem(RodoviariaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Viagem viagem)
    {
        _context.Viagens.Add(viagem);
        await _context.SaveChangesAsync();
    }

    public async Task<Viagem?> ObterPorIdAsync(int Id)
    {
        return await _context.Viagens.AsNoTracking()
            .Include(v => v.Rota).Include(v => v.Onibus).Include(v => v.Motorista)
            .FirstOrDefaultAsync(v => v.Id == Id);
    }

    public async Task<(List<Viagem> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina)
    {
        var query = _context.Viagens.AsNoTracking()
            .Include(v => v.Rota).Include(v => v.Onibus).Include(v => v.Motorista)
            .OrderBy(v => v.DataSaida);
        var total = await query.CountAsync();
        var itens = await query.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
        return (itens, total);
    }

    public async Task AtualizarAsync(Viagem viagem)
    {
        _context.Viagens.Update(viagem);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoverAsync(int Id)
    {
        var viagem = await _context.Viagens.FindAsync(Id);
        if (viagem is null) return false;
        _context.Viagens.Remove(viagem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TemReservasAsync(int Id)
    {
        return await _context.Reservas.AnyAsync(r => r.FkViagem == Id);
    }

    public async Task<bool> MotoristaTemViagemNoPeriodoAsync(int fkMotorista, DateTime dataSaida, DateTime dataChegada, int? idViagemExcluida = null)
    {
        return await _context.Viagens.AnyAsync(v =>
            v.FkMotorista == fkMotorista &&
            v.DataSaida < dataChegada &&
            v.DataChegada > dataSaida &&
            (!idViagemExcluida.HasValue || v.Id != idViagemExcluida.Value));
    }

    public async Task<bool> OnibusTemViagemNoPeriodoAsync(int fkOnibus, DateTime dataSaida, DateTime dataChegada, int? idViagemExcluida = null)
    {
        return await _context.Viagens.AnyAsync(v =>
            v.FkOnibus == fkOnibus &&
            v.DataSaida < dataChegada &&
            v.DataChegada > dataSaida &&
            (!idViagemExcluida.HasValue || v.Id != idViagemExcluida.Value));
    }

}

