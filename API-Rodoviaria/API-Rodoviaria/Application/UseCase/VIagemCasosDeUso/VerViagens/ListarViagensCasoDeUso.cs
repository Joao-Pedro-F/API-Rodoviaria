using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;
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

    public async Task<RespostaCriarViagemDTO> ExecutarAsync(int id) {

        var v = await _repositorioViagem.ObterPorIdAsync(id) ?? throw new KeyNotFoundException("Viagem não encontrada. ");

        return new RespostaCriarViagemDTO
        {
            Id = v.Id,
            FkOnibus = v.FkOnibus,
            Placa = v.Onibus.Placa,
            FkMotorista = v.FkMotorista,
            NomeMotorista = v.Motorista.Nome,
            FkRota = v.FkRota,
            EnderecoOrigem = v.Rota.EnderecoInicio,
            EnderecoFim = v.Rota.EnderecoFim,
            DataSaida = v.DataSaida,
            DataChegada = v.DataChegada,
        };
    }
    
        
    
}
