using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Requisicao.ViagemRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Infrastructure.DataAccess;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
using Microsoft.EntityFrameworkCore;
namespace API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.AtualizarViagem;
public class AtualizarViagemCasoDeUso : IAtualizarViagemCasoDeUso
{
    private readonly IRepositorioViagem _viagemRepositorio;
    private readonly IRepositorioMotorista _motoristaRepositorio;
    private readonly RodoviariaDbContext _context;

    public AtualizarViagemCasoDeUso(IRepositorioViagem viagemRepositorio, IRepositorioMotorista motoristaRepositorio, RodoviariaDbContext context)
    {
        _viagemRepositorio = viagemRepositorio;
        _motoristaRepositorio = motoristaRepositorio;
        _context = context;
    }

    public async Task<RespostaCriarViagemDTO> ExecutarAsync(int id, RequisicaoAtualizarViagemDTO requisicao)
    {
        if (requisicao.DataChegada <= requisicao.DataSaida)
        {
            throw new ExcecaoDeNegocio("A data de chegada deve ser posterior à data de saída.");
        }

        var viagem = await _viagemRepositorio.ObterPorIdAsync(id)
            ?? throw new ExcecaoDeNegocio("Viagem não encontrada.");

        int idMotoristaParaValidar = requisicao.FkMotorista ?? viagem.FkMotorista;


        var motorista = await _motoristaRepositorio.ObterPorIdAsync(idMotoristaParaValidar)
            ?? throw new ExcecaoDeNegocio("Motorista não encontrado.");

        var saida = requisicao.DataSaida != DateTime.MinValue ? requisicao.DataSaida : viagem.DataSaida;
        var chegada = requisicao.DataChegada != DateTime.MinValue ? requisicao.DataChegada : viagem.DataChegada;


        if (await _viagemRepositorio.MotoristaTemViagemNoPeriodoAsync(idMotoristaParaValidar, saida, chegada, id))
        {
            throw new ExcecaoDeNegocio("O motorista já possui uma viagem nesse período.");
        }

        if (await _viagemRepositorio.OnibusTemViagemNoPeriodoAsync(viagem.FkOnibus, saida, chegada, id))
        {
            throw new ExcecaoDeNegocio("O ônibus já está em outra viagem nesse período.");
        }


        viagem.FkMotorista = idMotoristaParaValidar;
        viagem.DataSaida = saida;
        viagem.DataChegada = chegada;

         _context.Viagem.Update(viagem);
        await _context.SaveChangesAsync();


        var viagemAtualizada = await _viagemRepositorio.ObterComDetalhesAsync(viagem.Id);

        return new RespostaCriarViagemDTO
        {
            Id = viagemAtualizada!.Id,
            FkOnibus = viagemAtualizada.FkOnibus,
            Placa = viagemAtualizada.Onibus?.Placa,
            FkMotorista = viagemAtualizada.FkMotorista,
            NomeMotorista = viagemAtualizada.Motorista?.Nome,
            FkRota = viagemAtualizada.FkRota,
            EnderecoOrigem = viagemAtualizada.Rota?.EnderecoInicio,
            EnderecoFim = viagemAtualizada.Rota?.EnderecoFim,
            DataSaida = viagemAtualizada.DataSaida,
            DataChegada = viagemAtualizada.DataChegada
        };
    }

}
