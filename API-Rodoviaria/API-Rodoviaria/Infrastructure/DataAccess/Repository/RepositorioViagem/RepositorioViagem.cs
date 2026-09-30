using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;
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
                .FirstOrDefaultAsync(v => v.Id == Id);
        }

        public async Task<bool> MotoristaTemViagemNoPeriodoAsync(int Id, DateTime saida, DateTime chegada)
        {
        return await _context.Viagens.AnyAsync(v =>
            v.FkMotorista == Id &&
            v.DataSaida < chegada &&
            v.DataChegada > saida);
        }

        public async Task<List<Viagem>> ListarProximaAsync()
        {
            return await _context.Viagens.AsNoTracking()
                .Include(v => v.Rota)
                .Include(v => v.Onibus)
                .Where(v => v.DataSaida > DateTime.UtcNow)
                .OrderBy(v => v.DataSaida)
                .ToListAsync();
        }
    }

