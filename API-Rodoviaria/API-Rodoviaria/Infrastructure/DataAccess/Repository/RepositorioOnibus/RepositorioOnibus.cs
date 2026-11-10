namespace API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;

using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

public class RepositorioOnibus : IRepositorioOnibus
{
     private readonly RodoviariaDbContext _context;

    public RepositorioOnibus(RodoviariaDbContext context)
        {
        _context = context;
    }

    public Task AdicionarAsync(Perfil perfil)
    {
        throw new ExcecaoDeNegocio("Não foi possível adicionar o ônibus.");
    }

    public async Task<bool> ExistePlacaAsync(string placa, int? idExcluir=null)
    {
        return await _context.Onibus.AnyAsync(o => o.Placa == placa && (idExcluir ==null || o.Id != idExcluir));
    }

    public Task<Perfil> ObterPorNomeAsync(string nomeCargo)
    {
        throw new ExcecaoDeNegocio("Não foi possível obter o ônibus por nome.");
    }
    public async Task<Onibus?> ObterPorIdAsync(int id)
    {
        return await _context.Onibus.AsNoTracking()
            .Include(v=> v.Viagens)
            .ThenInclude(m=>m.Motorista)
            .Include(o => o.Cadeiras)
            .FirstOrDefaultAsync(o=> o.Id == id);
    }

    public async Task<List<Cadeira>> ObterCadeirasPorNumeroAsync(int id, IEnumerable<int> numeros)
    {
        var lista= numeros.ToList();
        return await _context.Cadeiras.AsNoTracking()
            .Where(c => c.FkOnibus == id && lista.Contains(c.Numero))
            .ToListAsync();
    }

    public async Task<(List<RespostaCriarOnibusDTO> Itens, int Total)> ListarPaginadoAsync(int pagina, int tamanhoPagina)
    {
        var query = _context.Onibus.AsNoTracking()
            .Include(v => v.Viagens)
                .ThenInclude(r => r.Rota)
            .Include(v => v.Viagens)
                .ThenInclude(m => m.Motorista)
            .OrderBy(o => o.Id);

        var total = await query.CountAsync();

        // 1. Busca os dados como a entidade Onibus pura do banco
        var onibusBuscados = await query
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        // 2. Converte a lista pura para a lista de DTOs que o método exige retornar
        // Substitua 'onibus =>' pela sua lógica ou construtor do DTO
        var itens = onibusBuscados.Select(u => new RespostaCriarOnibusDTO
        {
            Id = u.Id,
            Placa = u.Placa,
            CapacidadeTotal = u.CapacidadeTotal,
            FkViagem = u.Viagens.FirstOrDefault()?.Id ?? 0,
            FkRota = u.Viagens.FirstOrDefault()?.FkRota ?? 0,
            FkMotorista = u.Viagens.FirstOrDefault()?.FkMotorista ?? 0,
            NomeMotorista = u.Viagens.FirstOrDefault()?.Motorista?.Nome ?? string.Empty,
            EnderecoInicio = u.Viagens.FirstOrDefault()?.Rota?.EnderecoInicio ?? string.Empty,
            EnderecoFim = u.Viagens.FirstOrDefault()?.Rota?.EnderecoFim ?? string.Empty,
            Datasaida = u.Viagens.FirstOrDefault()?.DataSaida ?? DateTime.MinValue,
            DataChegada = u.Viagens.FirstOrDefault()?.DataChegada ?? DateTime.MinValue


        }).ToList();

        return (itens, total);
    }



    public async Task AtualizarAsync(Onibus onibus)
    {
        _context.Onibus.Attach(onibus);
        _context.Entry(onibus).Property(o=> o.Placa).IsModified= true;
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> RemoverAsync(int id)
    {
        var onibus = await _context.Onibus.FindAsync(id);
        if (onibus == null)
        {
            return false;
        }
        _context.Onibus.Remove(onibus);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TemViagensAsync(int id)
    {
        return await _context.Viagem.AnyAsync(v => v.FkOnibus == id);
    }
    
   




}
