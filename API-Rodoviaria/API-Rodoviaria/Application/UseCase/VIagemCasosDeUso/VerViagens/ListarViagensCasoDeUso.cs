using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
namespace API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.VerViagens;

public class ListarViagensCasoDeUso : IListarViagensCasoDeUso
{
    private readonly IRepositorioViagem _repositorioViagem;

    public ListarViagensCasoDeUso(IRepositorioViagem repositorioViagem )
    {
        _repositorioViagem = repositorioViagem;
    }

    public async Task<List<RespostaCriarViagemDTO>> ExecutarAsync()
    {
        var viagens = await _repositorioViagem.ListarProximaAsync();

        return viagens.Select(v => new RespostaCriarViagemDTO
        {
            Id = v.Id,
            EnderecoOrigem = v.Rota.EnderecoInicio,
            EnderecoFim = v.Rota.EnderecoFim,
            DataSaida = v.DataSaida,
            DataChegada = v.DataChegada,
            Placa = v.Onibus.Placa,
            CapacidadeTotal = v.Onibus.CapacidadeTotal

        }).ToList();
    }
}
