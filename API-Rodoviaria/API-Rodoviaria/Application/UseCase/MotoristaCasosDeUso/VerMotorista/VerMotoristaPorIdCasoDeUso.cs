using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
namespace API_Rodoviaria.Application.UseCase.MotoristaCasosDeUso.VerMotorista;

public class VerMotoristaPorIdCasoDeUso : IVerMotoristaPorIdCasoDeUso
{
    private readonly IRepositorioMotorista _motoristaRepositorio;
    public VerMotoristaPorIdCasoDeUso(IRepositorioMotorista motoristaRepositorio)
    {
        _motoristaRepositorio = motoristaRepositorio;
    }
    public async Task<RespostaCriarMotoristaDTO> ExecutarAsync(int Id)
    {
        var motorista = await _motoristaRepositorio.ObterPorIdAsync(Id)
           ?? throw new KeyNotFoundException("Motorista não encontrado.");
        
        return new RespostaCriarMotoristaDTO
        {
            Id = motorista.Id,
            Nome = motorista.Nome,
            Cpf = motorista.Cpf,
            Cnh = motorista.Cnh
        };
    }
}
