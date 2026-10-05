using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Requisicao.ViagemRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;
using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
namespace API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.AtualizarViagem;
public class AtualizarViagemCasoDeUso : IAtualizarViagemCasoDeUso
{
    private readonly IRepositorioViagem _viagemRepositorio;
    private readonly IRepositorioMotorista _motoristaRepositorio;

    public AtualizarViagemCasoDeUso(IRepositorioViagem viagemRepositorio, IRepositorioMotorista motoristaRepositorio)
    {
        _viagemRepositorio = viagemRepositorio;
        _motoristaRepositorio = motoristaRepositorio;
    }

    public async Task<RespostaCriarViagemDTO> ExecutarAsync(int id, RequisicaoAtualizarViagemDTO requisicao)
    {
        if (requisicao.DataChegada <= requisicao.DataSaida)
        {
            throw new ArgumentException("A data de chegada deve ser posterior à data de saída.");
        }

        var viagem = await _viagemRepositorio.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException($"Viagem não encontrada.");

        var motorista = await _motoristaRepositorio.ObterPorIdAsync(viagem.FkMotorista)
            ?? throw new KeyNotFoundException("Motorista não encontrado.");

        var saida = requisicao.DataSaida;
        var chegada = requisicao.DataChegada;

        if (await _viagemRepositorio.MotoristaTemViagemNoPeriodoAsync(requisicao.FkMotorista, saida, chegada, id))
        {
            throw new InvalidOperationException("O motorista já possui uma viagem nesse período.");
        }

        if(await _viagemRepositorio.OnibusTemViagemNoPeriodoAsync(viagem.FkOnibus, saida, chegada, id))
        {
            throw new InvalidOperationException("O ônibus já está em outra viagem nesse período.");
        }

        viagem.FkMotorista = requisicao.FkMotorista;
        viagem.DataSaida = requisicao.DataSaida;
        viagem.DataChegada = requisicao.DataChegada;
        
        await _viagemRepositorio.AtualizarAsync(viagem);
        
        var atualizado = await _viagemRepositorio.ObterPorIdAsync(id);
        
        return new RespostaCriarViagemDTO
        {
            Id = atualizado!.Id,
            FkOnibus = atualizado.FkOnibus,
            Placa = atualizado.Onibus.Placa,
            FkMotorista = atualizado.FkMotorista,
            NomeMotorista = atualizado.Motorista.Nome,
            FkRota = atualizado.FkRota,
            EnderecoOrigem = atualizado.Rota.EnderecoInicio,
            EnderecoFim = atualizado.Rota.EnderecoFim,
            DataSaida = atualizado.DataSaida,
            DataChegada = atualizado.DataChegada


        };
    }

}
