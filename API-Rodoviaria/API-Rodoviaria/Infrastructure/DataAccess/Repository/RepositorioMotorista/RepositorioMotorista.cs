using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista
{
    public class RepositorioMotorista : IRepositorioMotorista
    {
        private readonly RodoviariaDbContext _context;
        public RepositorioMotorista(RodoviariaDbContext context)
        {
            _context = context;
        }
        public async Task AdicionarAsync(Motorista motorista)
        {
            _context.Motoristas.Add(motorista);
            await _context.SaveChangesAsync();
        }
        public async Task<Motorista?> ObterPorIdAsync(int Id)
        {
            return await _context.Motoristas.AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Id);
        }
        public async Task<bool> ExisteAsync(string cpf, string cnh, int? id)
        {
            return await _context.Motoristas.AnyAsync(m => (m.Cpf == cpf || m.Cnh ==
            cnh) && m.Id != id);
        }
        public async Task<(List<Motorista> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina)
        {
            var query = _context.Motoristas.AsNoTracking().OrderBy(m=>m.Id);
            var total = await query.CountAsync();
            var itens = await query.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
            return (itens, total);
        }
        public async  Task AtualizarAsync(Motorista motorista)
        {
            _context.Motoristas.Update(motorista);
            await _context.SaveChangesAsync();

        }
        public async Task<bool> RemoverAsync(int Id)
        {
            var motorista = await _context.Motoristas.FindAsync(Id);
            if (motorista == null)
            {
                return false;
            }
            _context.Motoristas.Remove(motorista);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TemViagensAsync(int Id)
        {
           return await _context.Viagens.AnyAsync(v => v.FkMotorista == Id);
        }
    }
}
