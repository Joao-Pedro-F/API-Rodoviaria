
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

    public async Task<Usuario?> ObterPorIdAsync(int id)
    {
        return await _context.Usuarios.AsNoTracking()
            .Include(u => u.Perfil)
            .FirstOrDefaultAsync(u => u.Id == id);
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

    public async Task<Perfil?> ObterPerfilPorNomeAsync(string cargo)
    {
        
        return await _context.Perfis.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Cargo.ToLower() == cargo.ToLower());
    }

    public async Task<(List<Usuario> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina)
    {
       var query= _context.Usuarios.AsNoTracking()
            .Include(u=> u.Perfil)
            .OrderBy(u => u.Id);

        var total = await query.CountAsync();
        var itens= await query
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return (itens, total);

    }

    public async Task AtualizarAsync(Usuario usuario)
    {
       _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return false;
        }
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TemReservasAsync(int id)
    {
        return await _context.Reservas.AnyAsync(r => r.FkUsuario == id);
    }

}
