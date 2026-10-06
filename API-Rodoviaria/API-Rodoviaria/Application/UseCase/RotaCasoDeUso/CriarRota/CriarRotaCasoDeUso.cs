
using API_Rodoviaria.Application.DTO_s.Requisicao.RotaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.ViagemRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IRota;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioRota;

namespace API_Rodoviaria.Application.UseCase.RotaCasoDeUso.CriarRota;

public class CriarRotaCasoDeUso : ICriarRotaCasoDeUso
{
    private readonly IRepositorioRota _repositorioRota;
    public CriarRotaCasoDeUso(IRepositorioRota repositorioRota)
    {
        _repositorioRota = repositorioRota;
    }
    public async Task<RespostaCriarRota> ExecutarAsync(RequisicaoCriarRotaDTO requisicao)
    {
        var rota = new Rota
        {
            EnderecoInicio = requisicao.EnderecoInicio,
            EnderecoFim = requisicao.EnderecoFim
        };

        await _repositorioRota.AdicionarAsync(rota);

        return new RespostaCriarRota
        {
            Id = rota.Id,
            EnderecoInicio = rota.EnderecoInicio,
            EnderecoFim = rota.EnderecoFim
        };
    }
}
