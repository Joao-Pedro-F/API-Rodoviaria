
using System.Data;
using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva
{
    public class RepositorioReserva : IRepositorioReserva
    {
        private readonly RodoviariaDbContext _context;
        public RepositorioReserva(RodoviariaDbContext context)
        {
            _context = context;
        }
        public async Task<Reserva?> ObterPorIdAsync(int idReserva)
        {
            return await _context.Reservas.AsNoTracking()
            .Include(r => r.ReservaCadeiras).ThenInclude(rc =>
            rc.Cadeira)
            .Include(r => r.Viagem)
            .FirstOrDefaultAsync(r => r.Id == idReserva);
        }
        public async Task<(List<Reserva> Itens, int Total)>
        ListarPaginadoAsync(int pagina, int tamanhoPagina)
        {
            var query = _context.Reservas.AsNoTracking()
            .Include(r => r.ReservaCadeiras).ThenInclude(rc =>
            rc.Cadeira)
            .Include(r => r.Viagem)
            .OrderByDescending(r => r.DataReserva);
            var total = await query.CountAsync();
            var itens = await query.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
            return (itens, total);
        }

        public async Task<bool> AdicionarSeCadeirasLivresAsync(Reserva reserva)
        {
            var idsCadeiras = reserva.ReservaCadeiras.Select(rc =>
            rc.FkCadeira).ToList();
            await using var transacao = await
            _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            try
            {
                var algumaOcupada = await _context.ReservaCadeiras.AnyAsync(rc =>  rc.Reserva.FkViagem == reserva.FkViagem && idsCadeiras.Contains(rc.FkCadeira));
                if (algumaOcupada) return false;
                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();
                await transacao.CommitAsync();
                return true;
            }
            catch (Exception ex) when (ex is DbUpdateException or
            PostgresException)
            {
                _context.ChangeTracker.Clear();
                return false;
            }
        }
        public async Task<bool> AtualizarCadeirasSeLivresAsync(int idReserva, List<int> novosIdsCadeiras)
        {
            await using var transacao = await
            _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            try
            {
                var reserva = await _context.Reservas
                .Include(r => r.ReservaCadeiras)
                .FirstOrDefaultAsync(r => r.Id == idReserva);

                if (reserva is null) return false;
                var algumaOcupada = await _context.ReservaCadeiras.AnyAsync(rc =>rc.Reserva.FkViagem == reserva.FkViagem &&rc.FkReserva != idReserva &&novosIdsCadeiras.Contains(rc.FkCadeira));
                if (algumaOcupada) return false;
                _context.ReservaCadeiras.RemoveRange(reserva.ReservaCadeiras);
                reserva.ReservaCadeiras = novosIdsCadeiras
                .Select(id => new ReservaCadeira
                {
                    FkReserva = idReserva,
                    FkCadeira = id
                })
                .ToList();
                await _context.SaveChangesAsync();
                await transacao.CommitAsync();
                return true;
            }
            catch (Exception ex) when (ex is DbUpdateException or
            PostgresException)
            {
                _context.ChangeTracker.Clear();
                return false;
            }
        }
        public async Task<bool> RemoverAsync(int idReserva)
        {
            var reserva = await _context.Reservas.FindAsync(idReserva);
            if (reserva is null) return false;
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<int>> ObterIdsCadeirasReservadasAsync(int idViagem)
        {
            return await _context.ReservaCadeiras.AsNoTracking()
                .Where(rc=> rc.Reserva.FkViagem== idViagem)
                .Select(rc=> rc.FkCadeira)
                .ToListAsync();
        }
    }
}
