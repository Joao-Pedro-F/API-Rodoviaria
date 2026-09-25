
using Microsoft.EntityFrameworkCore;
using API_Rodoviaria.Domain.Models;
namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario;

public class RepositorioUsuario : IRepositorioUsuario
{
    private readonly RodoviariaDbContext _context;

    public RepositorioUsuario(RodoviariaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<Usuario?> ObterPorUsernameAsync(string username)
    {
        var alvo= username.Trim().ToLower();
        return await _context.Usuarios.AsNoTracking()
            .Include(u => u.Perfil)
            .FirstOrDefaultAsync(u => u.Username.ToLower() == alvo);
    }

    public async Task<bool> ExisteAsync(string username, string email, string cpf)
    {
       var u= username.Trim().ToLower();
        var e = email.Trim().ToLower();
        return await _context.Usuarios.AnyAsync(x => x.Username.ToLower() == u || x.Email.ToLower() == e || x.Cpf == cpf);

    }
}
