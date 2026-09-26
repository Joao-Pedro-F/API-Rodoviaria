using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace API_Rodoviaria.Infrastructure.DataAccess.Repository;

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
    public async Task<bool> ExisteAsync(string cpf, string cnh)
    {
        return await _context.Motoristas.AnyAsync(m => m.Cpf == cpf || m.Cnh ==
        cnh);
    }
}