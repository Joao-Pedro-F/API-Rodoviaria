using API_Rodoviaria.Application.DTO_s.Requisicao.MotoristaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository;
using Microsoft.IdentityModel.Tokens;
using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;
namespace API_Rodoviaria.Application.UseCase;

public class CriarMotoristaCasoDeUso : ICriarOnibusCasoDeUso
{
    private readonly IRepositorioMotorista _repositorioMotorista;

    public CriarMotoristaCasoDeUso(IRepositorioMotorista repositorioMotorista)
    {
        _repositorioMotorista = repositorioMotorista;
    }
    public async Task<RespostaCriarMotoristaDTO>
    ExecutarAsync(RequisicaoCriarMotoristaDTO requisicao)
    {
        if (await _repositorioMotorista.ExisteAsync(requisicao.CPF,
        requisicao.Cnh))
            throw new ExcecaoDeNegocio("Já existe um motorista com esse CPF ou CNH.", 409);
            var motorista = new Motorista
            {
                Nome = requisicao.Nome.Trim(),
                Cpf = requisicao.Cpf,
                Cnh = requisicao.Cnh
            };
        await _repositorioMotorista.AdicionarAsync(motorista);
        return new RespostaCriarMotoristaDTO
        {
            Id = motorista.Id,
            Nome = motorista.Nome,
            Cpf = motorista.Cpf,
            Cnh = motorista.Cnh
        };
    }

    public Task<RespostaCriarOnibusDTO> ExecutarAsync(RequisicaoCriarOnibusDTO requisicao)
    {
        throw new NotImplementedException();
    }
}

