using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository;
using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;
using API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;
using System.Net.WebSockets;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
using API_Rodoviaria.Domain.Interfaces.IOnibus;

namespace API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.CriarOnibus;


public class CriarOnibusCasoDeUso : ICriarOnibusCasoDeUso
{
    private readonly IRepositorioOnibus _repositorioOnibus;
    private readonly IRepositorioMotorista _repositorioMotorista;
    private readonly IRepositorioViagem _repositorioViagem;

    public CriarOnibusCasoDeUso(
        IRepositorioOnibus repositorioOnibus,
        IRepositorioMotorista repositorioMotorista,
        IRepositorioViagem repositorioViagem)
    {
        _repositorioOnibus = repositorioOnibus;
        _repositorioMotorista = repositorioMotorista;
        _repositorioViagem = repositorioViagem;
    }
    public async Task<RespostaCriarOnibusDTO>
    ExecutarAsync(RequisicaoCriarOnibusDTO requisicao)
    {
        var saida = requisicao.DataSaida.UtcDateTime;
        var chegada = requisicao.DataChegada.UtcDateTime;

        if (chegada <= saida)
            throw new ExcecaoDeNegocio("A data de chegada deve ser depois da data de saida.");

        var placa = requisicao.Placa.Trim().ToUpperInvariant();

        if (await _repositorioOnibus.ExistePlacaAsync(placa))
            throw new ExcecaoDeNegocio("Já existe um onibus com essa placa", 409);

        var motorista = await _repositorioMotorista.ObterPorIdAsync(requisicao.FkMotorista)
            ?? throw new ExcecaoDeNegocio("Motorista não encontrado", 404);

        if (await _repositorioViagem.MotoristaTemViagemNoPeriodoAsync(motorista.Id, saida, chegada))
            throw new ExcecaoDeNegocio("Esse motorista já deve está em outra viagem nesse periodo", 409);

        var onibus = new Onibus
        {
            Placa = placa,
            CapacidadeTotal = requisicao.CapacidadeTotal,
            Cadeiras = Enumerable.Range(1, requisicao.CapacidadeTotal)
                       .Select(numero => new Cadeira { Numero = numero })
                       .ToList()
        };
       
           

        

        var viagem = new Viagem
        {
            DataSaida = saida,
            DataChegada = chegada,
            Onibus = onibus,
            FkMotorista = motorista.Id,
            Rota = new Rota 
            {
                EnderecoInicio = requisicao.EnderecoInicio.Trim(),
                EnderecoFim = requisicao.EnderecoFim.Trim()
            }
        };

        await _repositorioViagem.AdicionarAsync(viagem);
        return new RespostaCriarOnibusDTO
        {
            Id = onibus.Id,
            Placa = onibus.Placa,
            CapacidadeTotal = onibus.CapacidadeTotal,
            FkViagem = viagem.Id,
            FkRota = viagem.Rota.Id,
            FkMotorista = motorista.Id,
            EnderecoInicio = viagem.Rota.EnderecoInicio,
            EnderecoFim = viagem.Rota.EnderecoFim,
            Datasaida = viagem.DataSaida,
            DataChegada = viagem.DataChegada
        };
    }
}
