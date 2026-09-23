using API_Rodoviaria.Domain.Models;

namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioPerfil
{
    public class RepositorioPerfil(RodoviariaDbContext context) : IRepositorioPerfil
    {
        private readonly RodoviariaDbContext _context = context;

        public async Task CriarPerfil(Perfil perfil)
        {
            /* // Lógica para adicionar o perfil ao contexto e salvar no banco de dados
             _context.Perfis.Add(perfil);
             await _context.SaveChangesAsync();*/

            await Task.Delay(100); // Simulação de operação assíncrona
        }
    }
}
