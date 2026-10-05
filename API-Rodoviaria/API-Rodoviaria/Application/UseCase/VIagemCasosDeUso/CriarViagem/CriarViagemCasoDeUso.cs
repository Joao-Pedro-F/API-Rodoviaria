using API_Rodoviaria.Application.DTO_s.Requisicao.ViagemRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;
using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Domain.Interfaces.IMotorista;
using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
namespace API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.CriarViagem;

public class CriarViagemCasoDeUso : ICriarViagemCasoDeUso
{
    private readonly IRepositorioViagem _viagemRepositorio;
    private readonly IRepositorioMotorista _motoristaRepositorio;
    private readonly IRepositorioOnibus _onibusRepositorio;
    //private readonly IRepositorioRota _rotaRepositorio;

    public CriarViagemCasoDeUso(IRepositorioViagem viagemRepositorio, IRepositorioMotorista motoristaRepositorio, IRepositorioOnibus onibusRepositorio)//IRepositorioRota rotaRepositorio)
    {
        _viagemRepositorio = viagemRepositorio;
        _motoristaRepositorio = motoristaRepositorio;
        _onibusRepositorio = onibusRepositorio;
        //_rotaRepositorio = rotaRepositorio;
    }

    public async Task<RespostaCriarViagemDTO> ExecutarAsync(RequisicaoCriarViagemDTO requisicao)
    {
        if (requisicao.DataChegada <= requisicao.DataSaida)
        {
            throw new ArgumentException("A data de chegada deve ser posterior à data de saída.");
        }
        var motorista = await _motoristaRepositorio.ObterPorIdAsync(requisicao.MotoristaId)
            ?? throw new KeyNotFoundException("Motorista não encontrado.");

        var onibus = await _onibusRepositorio.ObterPorIdAsync(requisicao.OnibusId)
            ?? throw new KeyNotFoundException("Ônibus não encontrado.");
        
        //var rota = await _viagemRepositorio.ObterRotaPorIdAsync(requisicao.RotaId)
        //    ?? throw new KeyNotFoundException("Rota não encontrada.");

        var saida = requisicao.DataSaida;
        var chegada = requisicao.DataChegada;

        if (await _viagemRepositorio.MotoristaTemViagemNoPeriodoAsync(requisicao.MotoristaId, saida, chegada))
        {
            throw new InvalidOperationException("O motorista já possui uma viagem nesse período.");
        }

        if (await _viagemRepositorio.OnibusTemViagemNoPeriodoAsync(requisicao.OnibusId, saida, chegada))
        {
            throw new InvalidOperationException("O ônibus já está em outra viagem nesse período.");
        }

        var viagem = new Viagem
        {
            FkMotorista = requisicao.MotoristaId,
            FkOnibus = requisicao.OnibusId,
            FkRota = requisicao.RotaId,
            DataSaida = requisicao.DataSaida,
            DataChegada = requisicao.DataChegada
        };

        await _viagemRepositorio.AdicionarAsync(viagem);
        return new RespostaCriarViagemDTO
        {
            Id = viagem.Id,
            FkOnibus = viagem.FkOnibus,
            Placa = onibus.Placa,
            FkMotorista = viagem.FkMotorista,
            NomeMotorista = motorista.Nome,
            FkRota = viagem.FkRota,
            EnderecoOrigem = viagem.Rota?.EnderecoInicio ?? "Rota não encontrada",
            EnderecoFim = viagem.Rota?.EnderecoFim ?? "Rota não encontrada",
            DataSaida = viagem.DataSaida,
            DataChegada = viagem.DataChegada
        };
    }
}
