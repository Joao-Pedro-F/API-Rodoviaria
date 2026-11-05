using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioCadeira
{
    public class RepositorioCadeira : IRepositorioCadeira
    {
        private readonly RodoviariaDbContext _context;
        public RepositorioCadeira(RodoviariaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Cadeira>> ObterPorOnibusAsync(int idOnibus)
        {
            return await _context.Cadeiras.AsNoTracking()
            .Where(c => c.FkOnibus == idOnibus)
            .OrderBy(c => c.Numero)
            .ToListAsync();
        }
        public async Task<List<Cadeira>> ObterPorIdsAsync(IEnumerable<int>
        idsCadeiras)
        {
            var ids = idsCadeiras.ToList();
            return await _context.Cadeiras.AsNoTracking()
            .Include(c => c.Onibus)
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();

        }
        public async Task<(List<Cadeira> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina)
        {
            var query = _context.Cadeiras.AsNoTracking()
                .Include(o => o.Onibus)
                .OrderBy(c => c.Id);
            var total = await query.CountAsync();
            var itens = await query.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
            return (itens, total);
        }
    }
}
