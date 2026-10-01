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
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();

        }
    }
}
