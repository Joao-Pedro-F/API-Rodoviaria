using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
namespace API_Rodoviaria.Infrastructure.DataAccess;

public static class SementeInicial
{
    public static async Task ExecutarAsync(IServiceProvider servicos,
    IConfiguration config)
    {
        var db = servicos.GetRequiredService<RodoviariaDbContext>();
        var hash = servicos.GetRequiredService<IServicoHashSenha>();
        foreach (var nome in new[] { NomesPerfis.Admin, NomesPerfis.Cliente })
        {
            if (!await db.Perfis.AnyAsync(p => p.Cargo == nome))
                db.Perfis.Add(new Perfil { Cargo = nome });
        }
        await db.SaveChangesAsync();
        var perfilAdmin = await db.Perfis.FirstAsync(p => p.Cargo ==
        NomesPerfis.Admin);
        if (await db.Usuarios.AnyAsync(u => u.FkPerfil == perfilAdmin.Id))
            return;
        var secao = config.GetSection("AdminInicial");
        db.Usuarios.Add(new Usuario
        {
            Username = secao["Username"] ?? "admin",
            Password = hash.GerarhashSenha(secao["Password"] ?? "Admin@12345"),
            Email = secao["Email"] ?? "admin@rodoviaria.com",
            Cpf = secao["Cpf"] ?? "00000000000",
            Endereco = "Sistema",
            FkPerfil = perfilAdmin.Id
        });
        await db.SaveChangesAsync();
    }
}