using API_Rodoviaria.Application.DTO_s.Requisicao.MotoristaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Domain.Interfaces.IMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
namespace API_Rodoviaria.Application.UseCase.MotoristaCasosDeUso.AtualizarMotorista;

public class AtualizarMotoristaCasoDeUso : IAtualizarMotoristaCasoDeUso
{
    private readonly IRepositorioMotorista _motoristaRepositorio;

    public AtualizarMotoristaCasoDeUso(IRepositorioMotorista motoristaRepositorio)
    {
        _motoristaRepositorio = motoristaRepositorio;
    }

    public async Task<RequisicaoCriarMotoristaDTO> Executar(int Id, RequisicaoAtualizarMotoristaDTO requisicao)
    {
        var motorista = await _motoristaRepositorio.ObterPorIdAsync(Id) ?? throw new KeyNotFoundException("Motorista não encontrado");

        if (await _motoristaRepositorio.ExisteAsync(requisicao.Cpf, requisicao.Cnh,Id))
            throw new ExcecaoDeNegocio("Já existe outro motorista com o mesmo CPF ou CNH.");

        Id = motorista.Id;
        motorista.Nome = motorista.Nome;
        motorista.Cpf = motorista.Cpf;
        motorista.Cnh = motorista.Cnh ?? motorista.Cnh;

        await _motoristaRepositorio.AtualizarAsync(motorista);

        return new RequisicaoCriarMotoristaDTO
        {
            Nome = motorista.Nome,
            Cpf = motorista.Cpf,
            Cnh = motorista.Cnh
        };
    }
}
