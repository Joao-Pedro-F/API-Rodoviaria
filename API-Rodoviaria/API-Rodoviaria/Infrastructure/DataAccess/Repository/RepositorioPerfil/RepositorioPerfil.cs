using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioPerfil
{
    public class RepositorioPerfil : IRepositorioPerfil
    {
        private readonly RodoviariaDbContext _context ;

        public RepositorioPerfil(RodoviariaDbContext context)
        { 
            _context=context;        
        }

        public async Task AdicionarAsync(Perfil perfil)
        {
            _context.Perfis.Add(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task<Perfil?> ObterPorNomeAsync(string cargo)
        {
            var alvo= cargo.Trim().ToLower();
            return await _context.Perfis.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Cargo.ToLower() == alvo);
        }

    }
}
