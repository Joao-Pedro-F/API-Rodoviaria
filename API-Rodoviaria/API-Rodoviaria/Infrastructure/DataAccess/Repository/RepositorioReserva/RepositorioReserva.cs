
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

        public async Task<List<int>> ObterIdsCadeirasReservadasAsync(int idViagem)
        {
            return await _context.ReservaCadeiras.AsNoTracking()
                .Where(rc=>rc.Reserva.FkViagem==idViagem)
                .Select(rc=>rc.FkCadeira)
                .ToListAsync();
        }

        public async Task<bool> AdicionarSeCadeirasLivresAsync(Reserva reserva)
        {
            var idsCadeiras=reserva.ReservaCadeiras.Select(rc=>rc.FkCadeira).ToList();

            await using var transacao = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            try
            {
                var algumaOcupada = await _context.ReservaCadeiras.AnyAsync(rc => rc.Reserva.FkViagem == reserva.FkViagem && idsCadeiras.Contains(rc.FkCadeira));
                if (algumaOcupada)
                    return false;

                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();
                await transacao.CommitAsync();
                return true;
            }
            catch (Exception ex) when (ex is DbUpdateException or PostgresException)
            {
                _context.ChangeTracker.Clear();
                return false;
            }
        }
        
    }
}
